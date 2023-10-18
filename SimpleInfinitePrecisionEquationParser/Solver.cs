using System.Numerics;
using System.Reflection;

namespace SIPEP;

partial class Equation
{
    private static bool IsNumberOrVariable(int index, List<(SectionType, object)> data)
    {
        if (index < 0 || index >= data.Count)
            return false;
        if (data[index].Item1 == SectionType.Split)
            return false;
        if (data[index].Item1 == SectionType.Operators)
            return false;
        return true;
    }

    public BigComplex Solve()
    {
        var values = SolveValues();

        if (values.Length == 0)
            return 0;

        return SolveValues()[0];
    }

    public BigComplex[] SolveValues()
    {
        List<(SectionType type, object data)> currentData = new(this._data.Count);
        for (int i = 0; i < _data.Count; i++)
            currentData.Add(_data[i]);

        if (currentData.Count == 0)
            return Array.Empty<BigComplex>();

        if (currentData[0].type == SectionType.AssignVariable)
            return new BigComplex[] { SolveInstructional(currentData) };

        //first solve equations & functions
        SolveParenthesis(ref currentData);

        for (int currentPemdas = 0; currentPemdas <= FunctionLoader.HighestOperatorOrder; currentPemdas++)
            SolveOperators(ref currentData, currentPemdas);

        if (currentData.Count == 0)
            return Array.Empty<BigComplex>();

        List<BigComplex> outputs = new List<BigComplex>(currentData.Count);

        for (int i = 0; i < currentData.Count; i++)
        {
            if (currentData[i].type == SectionType.Split) 
                continue;
            outputs.Add(ToNumber(i, currentData));
        }

        return outputs.ToArray();
    }

    public bool SolveBoolean()
    {
        return Solve().BoolValue;
    }

    private void SolveParenthesis(ref List<(SectionType type, object data)> currentData)
    {
        for (int i = 0; i < currentData.Count; i++)
        {
            if (currentData[i].type == SectionType.Split)
                continue;
            if (currentData[i].type == SectionType.NestedEquation)
            {
                if (currentData[i].data is not string nestedEquationStr)
                    continue;
                currentData[i] = (SectionType.Number, new Equation(nestedEquationStr, Variables).Solve());
            }

            else if (currentData[i].type != SectionType.Function)
                continue;

            if (currentData.Count == i - 1)
                throw new InvalidEquationException();

            if (currentData[i].data is not string functionName)
                continue;
            if (currentData[i + 1].data is not string argsStr)
            {
                if (currentData[i + 1].data is BigComplex num && !FunctionLoader.IsStringFunction(functionName))
                {
                    // function(number)
                    currentData[i] = (SectionType.Number, FunctionLoader.DoFunction(functionName, num.ToString(), Variables));
                    currentData.RemoveAt(i + 1);
                }
                continue;
            }

            if (FunctionLoader.IsStringFunction(functionName))
                currentData[i] = (SectionType.Number, FunctionLoader.DoStringFunction(functionName, Variables, argsStr));
            else
                currentData[i] = (SectionType.Number, FunctionLoader.DoFunction(functionName, argsStr, Variables));

            currentData.RemoveAt(i + 1);
        }
    }

    private void SolveOperators(ref List<(SectionType, object)> currentData, int currentPemdas)
    {
        for (int i = 0; i < currentData.Count; i++)
        {
            if (currentData[i].Item1 != SectionType.Operators)
                continue;
            if (currentData[i].Item2 is not char operatorChar)
                continue;

            var (operatorInfo, methodInfo) = FunctionLoader.GetOperator(operatorChar);
            if (operatorInfo.Priority != currentPemdas)
                continue;

            DoOperator(ref currentData, ref i, operatorInfo, methodInfo);
        }
    }

    private void DoOperator(ref List<(SectionType, object)> currentData, ref int index, FunctionAttribute operatorInfo, MethodInfo methodInfo)
    {
        if (operatorInfo.OperatorStyle == OperatorStyle.None)
            return; // why even bother

        // 1 + 2
        if (CheckOperatorStyle(OperatorStyle.LeftAndRight, operatorInfo.OperatorStyle, index, currentData))
        {
            var args = new BigComplex[] { ToNumber(index - 1, currentData), ToNumber(index + 1, currentData) };
            var result = methodInfo.Invoke(null, new object[] { args });
            currentData[index] = (SectionType.Number, result);
            currentData.RemoveAt(index - 1);
            currentData.RemoveAt(index);
            index--;
            return;
        }

        // 5!
        if (CheckOperatorStyle(OperatorStyle.Left, operatorInfo.OperatorStyle, index, currentData))
        {
            var args = new BigComplex[] { ToNumber(index - 1, currentData) };
            var result = methodInfo.Invoke(null, new object[] { args });
            currentData[index] = (SectionType.Number, result);
            currentData.RemoveAt(index - 1);
            index--;
            return;
        }

        // -45
        if (CheckOperatorStyle(OperatorStyle.Right, operatorInfo.OperatorStyle, index, currentData))
        {
            var args = new BigComplex[] { ToNumber(index + 1, currentData) };
            var result = methodInfo.Invoke(null, new object[] { args });
            currentData[index] = (SectionType.Number, result);
            currentData.RemoveAt(index + 1);
            index--;
            return;
        }
    }

    private static bool CheckOperatorStyle(OperatorStyle target, OperatorStyle comparison, int index, List<(SectionType, object)> data)
    {
        if ((comparison & target) != target)
            return false;
        if ((target & OperatorStyle.LeftAndRight) == OperatorStyle.LeftAndRight)
        {
            if (IsNumberOrVariable(index - 1, data) && IsNumberOrVariable(index + 1, data))
                return true;
        }
        if ((target & OperatorStyle.Left) == OperatorStyle.Left)
        {
            if (IsNumberOrVariable(index - 1, data))
                return true;
        }
        if ((target & OperatorStyle.Right) == OperatorStyle.Right)
        {
            if (IsNumberOrVariable(index + 1, data))
                return true;
        }
        return false;
    }

    public BigComplex[] FindSolutions(string variable, double max, int depth, BigRational cutoff, bool realOnly)
    {
        var sniffer = new SnifferHandler(max, this, variable, depth, cutoff, realOnly);
        while (!sniffer.isDone) ;
        return sniffer.Output;
    }

    private BigComplex SolveInstructional(List<(SectionType type, object data)> currentData)
    {
        // let x, 5
        if (currentData.Count <= 1)
            return 0;

        if (currentData[0].data is not string varName)
            throw new InvalidEquationException();

        if (currentData[1].type == SectionType.Number)
        {
            if (currentData[1].data is not BigComplex number)
                throw new InvalidEquationException();

            SetVariable(varName, number);
            return number;
        }
        else if (currentData[1].type == SectionType.NestedEquation)
        {
            if (currentData[1].data is not string args)
                throw new InvalidEquationException();
            if (currentData[2].data is not string equation)
                throw new InvalidEquationException();

            FunctionLoader.AddFunction(varName, args, equation);
            return true;
        }

        throw new InvalidEquationException();
    }
}
