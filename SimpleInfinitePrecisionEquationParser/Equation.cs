using System.Numerics;

namespace SIPEP;

public class Equation
{
    public static bool Radians = true;
    public const int Version = 10;
    public static int DecimalPrecision { get => BigRational.MaxDigits; set => BigRational.MaxDigits = value; }

    public Dictionary<string, BigComplex> Variables = Constants.Vars;
    public string[] positionalVars = Array.Empty<string>();

    private List<(SectionType, object)> _data = new();

    public enum SectionType : byte
    {
        Number, //2, 424, 234i21, 12.3
        Variable, // pi, x, my_silly_variable_name
        Function, // sin, phase, rand
        Operators, // +, -, *, /, ^
        NestedEquation, // (2 + 1)
        Parameters, // sin ->(243, 12)<-
        AssignVariable // (x) = 3
    }

    public Equation(string equationStr, Dictionary<string, BigComplex>? variables = null)
    {
        FunctionLoader.ReloadFunctions();
        if (variables is not null)
        {
            foreach (var item in variables)
            {
                if (Variables.ContainsKey(item.Key))
                    Variables[item.Key] = item.Value;
                else
                    Variables.Add(item.Key, item.Value);
            }
        }

        var newVars = Conversions.All;
        foreach (var var in newVars)
        {
            if (Variables.ContainsKey(var.Key))
                continue;
            Variables.Add(var.Key, var.Value);
        }

        LoadString(equationStr);
    }

    public void SetVariable(string name, BigComplex data)
    {
        if (Variables.ContainsKey(name))
            Variables[name] = data;
        else
            Variables.Add(name, data);
    }

    private void Instructional(string str, ref List<(SectionType, object)> data)
    {
        str = str[4..]; //removes the "let"

        int indexOfEquals = -1;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != '=')
                continue;
            indexOfEquals = i;
            break;
        }

        if (indexOfEquals < 0)
            throw new InvalidEquationException();

        string varName = str[..(indexOfEquals)].Replace(" ", "");
        string equation = str[(indexOfEquals + 1)..];

        if (varName.EndsWith(")"))
        {
            // it's a function
            var functionName = varName.Split('(')[0];
            var args = varName.Split('(')[1][..^1];
            args = args.Trim();

            data = new List<(SectionType, object)>()
            {
                (SectionType.AssignVariable, functionName),
                (SectionType.Parameters, args),
                (SectionType.NestedEquation, equation),
            };

            return;
        }

        data = new List<(SectionType, object)>()
        {
            (SectionType.AssignVariable, varName),
            (SectionType.Number, new Equation(equation, Variables).Solve()),
        };
    }

    public void LoadString(string equationStr)
    {
        _data.Clear();
        if (equationStr.StartsWith("let "))
        {
            Instructional(equationStr, ref _data);
            return;
        }

        string current = "";
        int indentLevel = 0;

        for (int i = 0; i < equationStr.Length; i++)
        {
            if (equationStr[i] == ' ' && indentLevel == 0)
                continue;
            if (equationStr[i] == '(')
            {
                indentLevel++;

                if (indentLevel == 1)
                {
                    if (current != "")
                    {
                        var type = GetTypeOfPart(current, out object dat);
                        if (type == SectionType.Variable)
                            type = SectionType.Function;
                        _data.Add((type, dat));
                        current = "";
                    }
                }
                else
                    current += equationStr[i];
                continue;
            }

            if (equationStr[i] == ')')
            {
                indentLevel--;
                if (indentLevel == 0)
                {
                    SectionType paramsOrEquation = SectionType.NestedEquation;
                    if (_data.Count != 0 && _data[^1].Item1 == SectionType.Function)
                        paramsOrEquation = SectionType.Parameters;
                    _data.Add((paramsOrEquation, current));
                    current = "";
                    continue;
                }
                current += ")";
                continue;
            }

            if (indentLevel != 0)
            {
                current += equationStr[i];
                continue;
            }

            if (FunctionLoader.Operators.Contains(equationStr[i]))
            {
                if (equationStr[i] == '-')
                {
                    if (current.EndsWith("i"))
                    {
                        current += "-";
                        continue;
                    }

                    if (current.Length == 0)
                    {
                        if (i == 0) //example: -4 + 1
                        {
                            current += "-";
                            continue;
                        }
                        else if (_data.Count > 0 && _data[^1].Item1 == SectionType.Operators)
                        {
                            current += "-";
                            continue;
                        }
                    }
                }

                if (current != "")
                {
                    var type = GetTypeOfPart(current, out object dat);
                    _data.Add((type, dat));
                }
                current = "";
                _data.Add((SectionType.Operators, equationStr[i]));
                continue;
            }

            current += equationStr[i];
        }

        if (current != "")
        {
            var finaltype = GetTypeOfPart(current, out object finaldat);
            _data.Add((finaltype, finaldat));
        }

        if (indentLevel != 0)
            throw new InvalidEquationException();
    }

    public BigComplex Solve()
    {
        List<(SectionType, object)> currentData = new(this._data.Count);
        for (int i = 0; i < _data.Count; i++)
            currentData.Add(_data[i]);

        //first solve equations & functions
        for (int i = 0; i < currentData.Count; i++)
        {
            if (currentData[i].Item1 == SectionType.AssignVariable)
            {
                SolveInstructional(currentData, ref i);
                continue;
            }

            if (currentData[i].Item1 == SectionType.NestedEquation)
            {
                if (currentData[i].Item2 is not string nestedEquationStr)
                    continue;
                currentData[i] = (SectionType.Number, new Equation(nestedEquationStr, Variables).Solve());
            }

            else if (currentData[i].Item1 != SectionType.Function)
                continue;

            if (currentData.Count == i - 1)
                throw new InvalidEquationException();

            if (currentData[i].Item2 is not string functionName)
                continue;
            if (currentData[i + 1].Item2 is not string argsStr)
                continue;

            currentData[i] = (SectionType.Number, FunctionLoader.DoFunction(functionName, argsStr, Variables));
            currentData.RemoveAt(i + 1);
        }

        for (int currentPemdas = 0; currentPemdas <= FunctionLoader.HighestOperatorOrder; currentPemdas++)
        {
            for (int i = 0; i < currentData.Count; i++)
            {
                if (currentData[i].Item1 != SectionType.Operators)
                    continue;
                if (currentData[i].Item2 is not char operatorChar)
                    continue;

                var oper = FunctionLoader.GetOperator(operatorChar);
                if (oper.oper.Priority != currentPemdas)
                    continue;

                if (oper.oper.OperatorStyle == OperatorStyle.None)
                    continue; //why even bother

                // 1 + 2
                if ((oper.oper.OperatorStyle & OperatorStyle.LeftAndRight) == OperatorStyle.LeftAndRight)
                {
                    if (IsNumberOrVariable(i - 1, currentData) && IsNumberOrVariable(i + 1, currentData))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i - 1, currentData), ToNumber(i + 1, currentData)));
                        if (result is null)
                            continue;
                        currentData[i] = (SectionType.Number, result);
                        currentData.RemoveAt(i - 1);
                        currentData.RemoveAt(i);
                        i--;
                        continue;
                    }
                }

                // 5!
                if ((oper.oper.OperatorStyle & OperatorStyle.Left) == OperatorStyle.Left)
                {
                    if (IsNumberOrVariable(i - 1, currentData))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i - 1, currentData)));
                        if (result is null)
                            throw new InvalidEquationException();
                        currentData[i] = (SectionType.Number, result);
                        currentData.RemoveAt(i - 1);
                        i--;
                    }
                }

                // -45
                if ((oper.oper.OperatorStyle & OperatorStyle.Right) == OperatorStyle.Right)
                {
                    if (IsNumberOrVariable(i + 1, currentData))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i + 1, currentData)));
                        if (result is null)
                            throw new InvalidEquationException();
                        currentData[i] = (SectionType.Number, result);
                        currentData.RemoveAt(i + 1);
                        i--;
                    }
                }
            }
        }

        if (currentData.Count == 0)
            return BigComplex.Zero;

        if (currentData.Count != 1)
            throw new InvalidEquationException();

        return ToNumber(0, currentData);

        static object[] GetAsObjArray(params BigComplex[] nums)
        {
            return new object[] { nums }; //yup
        }
    }

    private void SolveInstructional(List<(SectionType, object)> currentData, ref int i)
    {
        if (currentData[i].Item2 is not string varName)
            throw new InvalidEquationException();
        if (i + 1 >= currentData.Count)
            throw new InvalidEquationException();

        if (currentData[i + 1].Item1 == SectionType.Number)
        {
            if (currentData[i + 1].Item2 is not BigComplex number)
                throw new InvalidEquationException();

            if (Variables.ContainsKey(varName))
                Variables[varName] = number;
            else
                Variables.Add(varName, number);
            currentData.RemoveAt(i);
            i--;
        }
        else if (currentData[i + 1].Item1 == SectionType.Parameters)
        {
            if (i + 2 >= currentData.Count)
                throw new InvalidEquationException();
            if (currentData[i + 1].Item2 is not string args)
                throw new InvalidEquationException();
            if (currentData[i + 2].Item2 is not string equation)
                throw new InvalidEquationException();

            FunctionLoader.AddFunction(varName, args, equation);
            currentData.RemoveAt(i + 1);
            currentData.RemoveAt(i + 1);
            currentData[i] = (SectionType.Number, new BigComplex(true));
        }
    }

    private static bool IsNumberOrVariable(int index, List<(SectionType, object)> data)
    {
        if (index < 0 || index >= data.Count)
            return false;
        return data[index].Item1 == SectionType.Number || data[index].Item1 == SectionType.Variable;
    }

    public bool SolveBoolean()
    {
        return Solve().BoolValue;
    }

    private BigComplex ToNumber(int index, List<(SectionType, object)> data)
    {
        if (data[index].Item1 == SectionType.Number)
        {
            if (data[index].Item2 is not BigComplex num)
                throw new InvalidEquationException();
            return num;
        }
        else if (data[index].Item1 == SectionType.Variable)
        {
            if (data[index].Item2 is not string varName)
                throw new InvalidEquationException();
            return GetVariable(varName);
        }
        throw new InvalidEquationException();
    }

    private BigComplex GetVariable(string name)
    {
        if (name.StartsWith("-"))
            return -Variables[name[1..]];
       return Variables[name];
    }

    private static SectionType GetTypeOfPart(string s, out object data)
    {
        data = s;
        if (s.StartsWith("(") && s.EndsWith(")"))
            return SectionType.NestedEquation;

        if (s == "-")
        {
            data = '-';
            return SectionType.Operators;
        }

        if (BigComplex.TryParse(s, out BigComplex numData))
        {
            data = numData;
            return SectionType.Number;
        }

        return SectionType.Variable; //functions get converted later
    }
}
