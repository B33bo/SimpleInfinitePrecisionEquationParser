using System.Numerics;

namespace SIPEP.Functions;

public static class Fractal
{
    [Function("Julia")]
    public static BigComplex Julia(params BigComplex[] args)
    {
        if (args.Length < 2)
            return 0;
        return args[0] * args[0] + args[1];
    }

    [Function("BurningShip")]
    public static BigComplex BurningShip(params BigComplex[] args)
    {
        if (args.Length < 2)
            return 0;
        var x = new BigComplex(BigRational.Abs(args[0].Real), BigRational.Abs(args[0].Imaginary));
        return x * x + args[1];
    }
}
