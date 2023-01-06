using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Tests.Functions;

[TestClass]
public class Misc
{
    [TestMethod]
    public void Abs()
    {
        Assert.AreEqual(1, new Equation(@"abs(-1)").Solve().Real);
        Assert.AreEqual(1, new Equation(@"abs(1)").Solve().Real);
        Assert.AreEqual(1, new Equation(@"abs(-1, 2)").Solve().Real);
        Assert.AreEqual(1, new Equation(@"abs(i)").Solve().Real);
        Assert.AreEqual(0, new Equation(@"abs()").Solve().Real);

        var abs1 = new Equation(@"abs(5i5)").Solve().Real;
        Assert.IsTrue(abs1 > 7 && abs1 < 7.1);
    }

    [TestMethod]
    public void AbsSigned()
    {
        Assert.AreEqual(-1, new Equation(@"abssigned(-1)").Solve().Real);
        Assert.AreEqual(1, new Equation(@"abssigned(1)").Solve().Real);
        Assert.AreEqual(1, new Equation(@"abssigned(i)").Solve().Real);
        Assert.AreEqual(-1, new Equation(@"abssigned(-i)").Solve().Real);
    }

    [TestMethod]
    public void Log()
    {
        var num1 = new Equation("log(4)").Solve().Real;
        var num2 = new Equation("log(2, 2)").Solve().Real;
        var num3 = new Equation("log(8, 2)").Solve().Real;
        var num4 = new Equation("log(3i5, 3)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("log()").Solve());

        Assert.IsTrue(num1 > .6 && num1 < .7);
        Assert.IsTrue(num2 > .9 && num2 < 1.1);
        Assert.IsTrue(num3 > 2.9 && num3 < 3.1);
        Assert.IsTrue(num4.Real > 1.6 && num4.Real < 1.7 && num4.Imaginary > .9 && num4.Imaginary < 1.1);
    }

    [TestMethod]
    public void Exp()
    {
        var num1 = new Equation("exp(4)").Solve().Real;
        var num2 = new Equation("exp(0)").Solve().Real;
        var num3 = new Equation("exp()").Solve().Real;
        var num4 = new Equation("exp(3i5)").Solve();

        Assert.IsTrue(num1 > 54.5 && num1 < 54.6);
        Assert.IsTrue(num2 > .9 && num2 < 1.1);
        Assert.IsTrue(num3 > 2.7 && num3 < 2.8);
        Assert.IsTrue(num4.Real > 5.6 && num4.Real < 5.8 && num4.Imaginary > -19.3 && num4.Imaginary < -19.2);
    }

    [TestMethod]
    public void Rand()
    {
        var num1 = new Equation("rand(4)").Solve().Real;
        var num2 = new Equation("rand(2, 10)").Solve().Real;
        var num3 = new Equation("rand(0, 10, 1)").Solve().Real;
        var num4 = new Equation("rand(1i2, 8i9)").Solve();
        var num5 = new Equation("rand()").Solve();

        Assert.IsTrue(num1 >= 0 && num1 <= 4);
        Assert.IsTrue(num2 >= 2 && num2 <= 10);
        Assert.IsTrue(num3 > 4.6 && num3 < 4.7);
        Assert.IsTrue(num4.Real >= 1 && num4.Real <= 8 && num4.Imaginary >= 2 && num4.Imaginary <= 9);
        Assert.IsTrue(num5.Real >= 0 && num5.Real <= 1 && num5.Imaginary == 0);
    }

    [TestMethod]
    public void Crash()
    {
        try
        {
            new Equation("crash()").Solve();
        }
        catch (Exception)
        {
            return;
        }
        Assert.Fail();
    }

    [TestMethod]
    public void MaxDigits()
    {
        Assert.AreEqual(BigRational.MaxDigits.ToString(), new Equation("MaxDigits()").Solve().ToString());
        var maxDigits = BigRational.MaxDigits;
        Assert.AreEqual("10", new Equation("MaxDigits(10)").Solve().ToString());
        BigRational.MaxDigits = maxDigits;
    }

    [TestMethod]
    public void Echo()
    {
        Assert.AreEqual(new BigComplex(12, 0), new Equation("Echo(12)").Solve());
    }

    [TestMethod]
    public void Min()
    {
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Min()").Solve());
    }

    [TestMethod]
    public void Max()
    {
        Assert.AreEqual(new BigComplex(14, 0), new Equation("Max(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Max(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Max()").Solve());
    }

    [TestMethod]
    public void Mean()
    {
        Assert.AreEqual(new BigComplex(BigRational.Parse("5"), 0), new Equation("Mean(2,5, 8)").Solve());
        Assert.AreEqual(new BigComplex(BigRational.Parse("7.125"), 0), new Equation("Mean(.5,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Mean(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Mean()").Solve());
    }

    [TestMethod]
    public void Len()
    {
        Assert.AreEqual(new BigComplex(5, 0), new Equation("Len(.5, 12, 2, 14, pi)").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Len(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Len()").Solve());
    }

    [TestMethod]
    public void Real()
    {
        Assert.AreEqual(new BigComplex(BigRational.Parse(".5"), 0), new Equation("Real(.5)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Real(3i2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Real()").Solve());
    }

    [TestMethod]
    public void Imaginary()
    {
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Imaginary(.5)").Solve());
        Assert.AreEqual(new BigComplex(0, 2), new Equation("Imaginary(3i2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Imaginary()").Solve());
    }

    [TestMethod]
    public void Round()
    {
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Round(.5)").Solve());
        Assert.AreEqual(new BigComplex(3, 3), new Equation("Round(3.1i2.5, 3)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Round(.5, 2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Round()").Solve());
    }

    [TestMethod]
    public void Floor()
    {
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Floor(.9)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Floor(.2)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Floor(5, 3)").Solve());
        Assert.AreEqual(new BigComplex(3, 3), new Equation("Floor(4.5i4, 3)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Floor(.5, 2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Floor()").Solve());
    }

    [TestMethod]
    public void Ceil()
    {
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Ceiling(.9)").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Ceiling(.2)").Solve());
        Assert.AreEqual(new BigComplex(6, 0), new Equation("Ceiling(4.5, 3)").Solve());
        Assert.AreEqual(new BigComplex(6, 6), new Equation("Ceiling(4.5i4, 3)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Ceiling(.5, 2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Ceiling()").Solve());
    }

    [TestMethod]
    public void Halfway()
    {
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Halfway(1, 5)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Halfway(0, 6)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Halfway()").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Halfway(2)").Solve());
        Assert.AreEqual(new BigComplex(1, 1), new Equation("Halfway(2, 0i2)").Solve());
    }

    [TestMethod]
    public void Lerp()
    {
        Assert.AreEqual(new BigComplex(BigRational.Parse("1.8"), 0), new Equation("Lerp(1, 5, .2)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Lerp(0, 6, .5)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Lerp()").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Lerp(2)").Solve());
        Assert.AreEqual(new BigComplex(5, 0), new Equation("Lerp(0, 10)").Solve());
        Assert.AreEqual(new BigComplex(0, BigRational.Parse(".2")), new Equation("Lerp(0, i, .2)").Solve());
    }

    [TestMethod]
    public void Convert()
    {
        Assert.AreEqual(new BigComplex(10, 0), new Equation("Convert(1, centimeter, millimeter)").Solve());
        Assert.AreEqual(new BigComplex(0, 10), new Equation("Convert(i, centimeter, millimeter)").Solve());
        Assert.AreEqual(new BigComplex(500, 0), new Equation("Convert(5, centimeter)").Solve());
        Assert.AreEqual(new BigComplex(5, 0), new Equation("Convert(5)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Convert()").Solve());
    }

    [TestMethod]
    public void ConvertTemperature()
    {
        var ans1 = new Equation("ConvertTemperature(1, fahrenheit, kelvin)").Solve();
        var ans2 = new Equation("ConvertTemperature(1, celsius, kelvin)").Solve();
        var ans3 = new Equation("ConvertTemperature(5, kelvin)").Solve();

        Assert.IsTrue(ans1.Real > 255 && ans1.Real < 256);
        Assert.IsTrue(ans2.Real > 274 && ans2.Real < 275);
        Assert.IsTrue(ans3.Real > 278 && ans3.Real < 279);
        Assert.AreEqual(new BigComplex(5, 0), new Equation("ConvertTemperature(5)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("ConvertTemperature()").Solve());
    }

    [TestMethod]
    public static void ToDegrees()
    {
        var ans1 = new Equation("ToDegrees(pi)").Solve().Real;
        var ans2 = new Equation("ToDegrees(pi * i)").Solve().Imaginary;
        Assert.IsTrue(ans1 > 179 && ans1 < 181);
        Assert.IsTrue(ans2 > 179 && ans2 < 181);
        Assert.AreEqual(BigComplex.Zero, new Equation("ToDegrees()").Solve().Imaginary);
    }
}
