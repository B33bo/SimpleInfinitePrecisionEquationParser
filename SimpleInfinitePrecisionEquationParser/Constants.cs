using System.Numerics;

namespace SIPEP;

internal static class Constants
{
    public static BigComplex E = BigRational.Parse("2.718281828459045");
    public static BigComplex Pi = BigRational.Pi(Equation.DecimalPrecision);

    public static Dictionary<string, Variable> Vars => new()
    {
        { "e", E },
        { "pi", Pi },
        { "tau", Pi * 2 },
        { "phi", (BigComplex)BigRational.Parse("1.618033988749894") },
        { "i", new BigComplex(0, 1) },
        { "NaN", BigComplex.NaN },
        { "true", BigComplex.True },
        { "false", BigComplex.False },
        { "version", (BigComplex)Equation.Version },
        { "inf", BigComplex.Infinity },
    };
}
