using System.Numerics;

namespace SIPEP;

internal static class Constants
{
    public static BigRational E = BigRational.Parse("2.718281828459045");
    public static BigRational Pi = BigRational.Pi(Equation.DecimalPrecision);

    public static Dictionary<string, BigComplex> Vars => new()
    {
        { "e", E },
        { "pi", Pi },
        { "tau", Pi * 2 },
        { "phi", BigRational.Parse("1.618033988749894") },
        { "i", new(0, 1) },
        { "one", 1 },
        { "zero", 0 },
        { "NaN", BigComplex.Zero / BigComplex.Zero },
        { "true", BigComplex.True },
        { "false", BigComplex.False },
        { "version", Equation.Version },
    };
}
