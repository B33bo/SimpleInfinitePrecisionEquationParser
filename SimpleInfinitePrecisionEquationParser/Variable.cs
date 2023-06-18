using System.Numerics;

namespace SIPEP;

public struct Variable
{
    public BigComplex Data;
    public bool IsConstant;

    public Variable(BigComplex value, bool isConstant)
    {
        Data = value;
        IsConstant = isConstant;
    }

    public static implicit operator Variable(BigComplex value)
    {
        return new Variable(value, true);
    }
    
    public static implicit operator Variable(int value)
    {
        return new Variable(value, true);
    }
    
    public static implicit operator Variable(BigRational value)
    {
        return new Variable(value, true);
    }

    public static implicit operator BigComplex(Variable value)
    {
        return value.Data;
    }

    public override string ToString()
    {
        return Data.ToString();
    }
}
