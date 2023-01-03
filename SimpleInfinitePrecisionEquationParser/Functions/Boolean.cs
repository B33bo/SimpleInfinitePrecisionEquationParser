using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Functions;

public static class Boolean
{
    [Function("And", Operator = '&', Priority = 2)]
    public static BigComplex And(params BigComplex[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (!args[i].BoolValue)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Or", Operator = '|', Priority = 3)]
    public static BigComplex Or(params BigComplex[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i].BoolValue)
                return BigComplex.True;
        }
        return BigComplex.False;
    }

    [Function("Xor")]
    public static BigComplex Xor(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.False;
        bool checkingVal = args[0].BoolValue;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i].BoolValue == checkingVal)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Not", Operator = '¬', OperatorStyle = OperatorStyle.Right, Priority = 1)]
    public static BigComplex Not(params BigComplex[] args)
    {
        if (args.Length == 0)
            return BigComplex.True;
        return new BigComplex(!args[0].BoolValue);
    }

    [Function("Equals", Operator = '=', Priority = 5)]
    public static BigComplex Equals(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.True;
        for (int i = 1; i < args.Length; i++)
        {
            if (args[i] != args[i - 1])
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Greater", Operator = '>', Priority = 5)]
    public static BigComplex Greater(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.False;

        for (int i = 1; i < args.Length; i++)
        {
            if (Misc.Abs(args[i]).Real >= Misc.Abs(args[i - 1]).Real)
                return BigComplex.False;
        }
        return BigComplex.True;
    }

    [Function("Less", Operator = '<', Priority = 5)]
    public static BigComplex Less(params BigComplex[] args)
    {
        if (args.Length == 1 || args.Length == 0)
            return BigComplex.False;
        for (int i = 1; i < args.Length; i++)
        {
            if (Misc.Abs(args[i]).Real <= Misc.Abs(args[i - 1]).Real)
                return BigComplex.False;
        }
        return BigComplex.True;
    }
}
