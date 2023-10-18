namespace SIPEP;

partial class Equation
{
    public void Simplify()
    {
        bool didchange;
        do
        {
            didchange = false;
            for (int i = 0; i < _data.Count; i++)
            {
                if (SimplifyAt(ref i))
                    didchange = true;
            }
            ;
        } while (didchange);
    }

    private bool SimplifyAt(ref int i)
    {
        switch (_data[i].type)
        {
            case SectionType.Variable:
                if (IsVariableConstant(_data[i].data))
                {
                    _data[i] = (SectionType.Number, GetVariable(_data[i].data.ToString()));
                    return true;
                }
                return false;
            case SectionType.NestedEquation:
                if (!IsEquationConstant(_data[i].data))
                    return false;
                var equation = new Equation(_data[i].data.ToString(), Variables);
                _data[i] = (SectionType.Number, equation.Solve());
                return true;
            default:
                return false;
        }
    }

    public bool IsConstant => IsConst();

    private bool IsVariableConstant(object data)
    {
        if (data is not string varName)
            throw new InvalidCastException();
        if (!Variables.ContainsKey(varName))
            return false;
        if (!Variables[varName].IsConstant)
            return false;
        return true;
    }

    private static bool IsFunctionConstant(object data)
    {
        if (data is not string funcName)
        {
            if (data is not char oper)
                throw new InvalidCastException();
            return FunctionLoader.GetOperator(oper).FunctionInfo.StaticFunction;
        }
        return FunctionLoader.IsFunctionStatic(funcName);
    }

    private bool IsEquationConstant(object data)
    {
        if (data is not string equationStr)
            throw new InvalidCastException();
        return new Equation(equationStr, Variables).IsConstant;
    }

    private bool IsConst()
    {
        for (int i = 0; i < _data.Count; i++)
        {
            switch (_data[i].type)
            {
                case SectionType.Number:
                    break;
                case SectionType.Variable:
                    if (!IsVariableConstant(_data[i].data))
                        return false;
                    break;
                case SectionType.Function:
                    if (!IsFunctionConstant(_data[i].data))
                        return false;
                    break;
                case SectionType.Operators:
                    if (!IsFunctionConstant(_data[i].data))
                        return false;
                    break;
                case SectionType.NestedEquation:
                    if (!IsEquationConstant(_data[i].data))
                        return false;
                    break;
                case SectionType.AssignVariable:
                    return false;
                default:
                    return false;
            }
        }

        return true;
    }
}
