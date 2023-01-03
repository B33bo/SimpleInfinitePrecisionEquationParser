using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP;

public class Equation
{
    public static int DecimalPrecision { get => BigRational.MaxDigits; set => BigRational.MaxDigits = value; }

    public Dictionary<string, BigComplex> Variables = Constants.Vars;

    private List<(SectionType, object)> data = new();

    public enum SectionType
    {
        Number, //2, 424, 234i21, 12.3
        Variable, // pi, x, my_silly_variable_name
        Function, // sin, phase, rand
        Operators, // +, -, *, /, ^
        NestedEquation, // (2 + 1)
        Parameters, // sin ->(243, 12)<-
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

        string current = "";
        int indentLevel = 0;

        for (int i = 0; i < equationStr.Length; i++)
        {
            if (equationStr[i] == ' ')
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
                        data.Add((type, dat));
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
                    if (data.Count != 0 && data[^1].Item1 == SectionType.Function)
                        paramsOrEquation = SectionType.Parameters;
                    data.Add((paramsOrEquation, current));
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
                if (equationStr[i] == '-' && current.Length == 0)
                {
                    if (i == 0) //example: -4 + 1
                    {
                        current += "-";
                        continue;
                    }
                    else if (data.Count > 0 && data[^1].Item1 == SectionType.Operators)
                    {
                        current += "-";
                        continue;
                    }
                }

                if (current != "")
                {
                    var type = GetTypeOfPart(current, out object dat);
                    data.Add((type, dat));
                }
                current = "";
                data.Add((SectionType.Operators, equationStr[i]));
                continue;
            }

            current += equationStr[i];
        }

        if (current != "")
        {
            var finaltype = GetTypeOfPart(current, out object finaldat);
            data.Add((finaltype, finaldat));
        }

        ;
    }

    public BigComplex Solve()
    {
        //first solve equations & functions
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].Item1 == SectionType.NestedEquation)
            {
                if (data[i].Item2 is not string nestedEquationStr)
                    continue;
                data[i] = (SectionType.Number, new Equation(nestedEquationStr, Variables).Solve());
            }

            else if (data[i].Item1 != SectionType.Function)
                continue;

            if (data.Count == i - 1)
                throw new InvalidEquationException();

            if (data[i].Item2 is not string functionName)
                continue;
            if (data[i + 1].Item2 is not string argsStr)
                continue;

            data[i] = (SectionType.Number, FunctionLoader.DoFunction(functionName, argsStr, Variables));
            data.RemoveAt(i + 1);
        }

        for (int currentPemdas = 0; currentPemdas <= FunctionLoader.HighestOperatorOrder; currentPemdas++)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Item1 != SectionType.Operators)
                    continue;
                if (data[i].Item2 is not char operatorChar)
                    continue;

                var oper = FunctionLoader.GetOperator(operatorChar);
                if (oper.oper.Priority != currentPemdas)
                    continue;

                if (oper.oper.OperatorStyle == OperatorStyle.None)
                    continue; //why even bother

                // 1 + 2
                if ((oper.oper.OperatorStyle & OperatorStyle.LeftAndRight) == OperatorStyle.LeftAndRight)
                {
                    if (IsNumberOrVariable(i - 1) && IsNumberOrVariable(i + 1))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i - 1), ToNumber(i + 1)));
                        if (result is null)
                            continue;
                        data[i] = (SectionType.Number, result);
                        data.RemoveAt(i - 1);
                        data.RemoveAt(i);
                        i--;
                        continue;
                    }
                }

                // 5!
                if ((oper.oper.OperatorStyle & OperatorStyle.Left) == OperatorStyle.Left)
                {
                    if (IsNumberOrVariable(i - 1))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i - 1)));
                        if (result is null)
                            throw new InvalidEquationException();
                        data[i] = (SectionType.Number, result);
                        data.RemoveAt(i - 1);
                        i--;
                    }
                }

                // -45
                if ((oper.oper.OperatorStyle & OperatorStyle.Right) == OperatorStyle.Right)
                {
                    if (IsNumberOrVariable(i + 1))
                    {
                        var result = oper.method.Invoke(null, GetAsObjArray(ToNumber(i + 1)));
                        if (result is null)
                            throw new InvalidEquationException();
                        data[i] = (SectionType.Number, result);
                        data.RemoveAt(i + 1);
                        i--;
                    }
                }
            }
        }

        if (data.Count == 0)
            return BigComplex.Zero;

        if (data.Count != 1)
            throw new InvalidEquationException();

        return ToNumber(0);

        object[] GetAsObjArray(params BigComplex[] nums)
        {
            return new object[] { nums }; //yup
        }

        bool IsNumberOrVariable(int index)
        {
            if (index < 0 || index >= data.Count)
                return false;
            return data[index].Item1 == SectionType.Number || data[index].Item1 == SectionType.Variable;
        }
    }

    public bool SolveBoolean()
    {
        return Solve().BoolValue;
    }

    private BigComplex ToNumber(int index)
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

        if (BigComplex.TryParse(s, out BigComplex numData))
        {
            data = numData;
            return SectionType.Number;
        }

        return SectionType.Variable; //functions get converted later
    }
}
