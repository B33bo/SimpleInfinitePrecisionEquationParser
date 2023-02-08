using System.Numerics;

namespace SIPEP.Tests.Functions;

[TestClass]
public class Misc
{
    [TestMethod]
    public void Abs()
    {
        Assert.AreEqual(1, new Equation("abs(-1)").Solve().Real);
        Assert.AreEqual(1, new Equation("abs(1)").Solve().Real);
        Assert.AreEqual(1, new Equation("abs(-1, 2)").Solve().Real);
        Assert.AreEqual(1, new Equation("abs(i)").Solve().Real);
        Assert.AreEqual(0, new Equation("abs()").Solve().Real);
        Assert.AreEqual(BigComplex.Infinity, new Equation("abs(-inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("abs(inf)").Solve());

        var abs1 = new Equation("abs(5i5)").Solve().Real;
        Assert.IsTrue(abs1 > 7 && abs1 < 7.1);
    }

    [TestMethod]
    public void AbsSigned()
    {
        Assert.AreEqual(-1, new Equation("abssigned(-1)").Solve().Real);
        Assert.AreEqual(1, new Equation("abssigned(1)").Solve().Real);
        Assert.AreEqual(1, new Equation("abssigned(i)").Solve().Real);
        Assert.AreEqual(-1, new Equation("abssigned(-i)").Solve().Real);
        Assert.AreEqual(BigComplex.Infinity, new Equation("abssigned(inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("abssigned(-inf)").Solve());
    }

    [TestMethod]
    public void Sign()
    {
        Assert.AreEqual(-1, new Equation("sign(-3)").Solve().Real);
        Assert.AreEqual(1, new Equation("sign(1)").Solve().Real);
        Assert.AreEqual(new BigComplex(0, -1), new Equation("sign(i)").Solve());
        Assert.AreEqual(-1, new Equation("sign(-inf)").Solve());
        Assert.AreEqual(1, new Equation("sign(inf)").Solve());
    }

    [TestMethod]
    public void Log()
    {
        var num1 = new Equation("log(4)").Solve().Real;
        var num2 = new Equation("log(2, 2)").Solve().Real;
        var num3 = new Equation("log(8, 2)").Solve().Real;
        var num4 = new Equation("log(3i5, 3)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("log()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("log(inf)").Solve());

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
        Assert.AreEqual(BigComplex.Infinity, new Equation("exp(inf)").Solve());
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
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Echo()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Echo(inf)").Solve());
    }

    [TestMethod]
    public void Min()
    {
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Min()").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Min(3, -inf, 5)").Solve());
    }

    [TestMethod]
    public void Max()
    {
        Assert.AreEqual(new BigComplex(14, 0), new Equation("Max(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Max(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Max()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Max(3, inf, 5)").Solve());
    }

    [TestMethod]
    public void Mean()
    {
        Assert.AreEqual(new BigComplex(BigRational.Parse("5"), 0), new Equation("Mean(2,5, 8)").Solve());
        Assert.AreEqual(new BigComplex(BigRational.Parse("7.125"), 0), new Equation("Mean(.5,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Mean(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Mean()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Mean(0, 2, inf)").Solve());
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
        Assert.AreEqual(BigComplex.Infinity, new Equation("Round(inf)").Solve());
        Assert.AreEqual(0, new Equation("Round(2, inf)").Solve());
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

        Assert.AreEqual(BigComplex.Infinity, new Equation("Floor(inf)").Solve());
        Assert.AreEqual(0, new Equation("Floor(2, inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Floor(-1, inf)").Solve());
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

        Assert.AreEqual(BigComplex.Infinity, new Equation("Ceiling(inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Ceiling(2, inf)").Solve());
        Assert.AreEqual(0, new Equation("Ceiling(-1, inf)").Solve());
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
        Assert.AreEqual(BigComplex.Infinity, new Equation("Lerp(0, inf, .2)").Solve());
        Assert.AreEqual(0, new Equation("Lerp(0, inf, 0)").Solve());
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
        Assert.AreEqual(BigComplex.Infinity, new Equation("ConvertTemperature(inf, fahrenheit, kelvin)").Solve());
    }

    [TestMethod]
    public void Remainder()
    {
        Assert.AreEqual(1, new Equation("Remainder(10,3)").Solve());
        Assert.AreEqual(-1, new Equation("Remainder(-10,3)").Solve());
        Assert.AreEqual(0, new Equation("Remainder(inf,3)").Solve());
    }

    [TestMethod]
    public void Int()
    {
        Assert.AreEqual(10, new Equation("Int(10.3)").Solve());
        Assert.AreEqual(10, new Equation("Int(10)").Solve());
        Assert.AreEqual(0, new Equation("Int(0)").Solve());
        Assert.AreEqual(new BigComplex(0, -4), new Equation("Int(0.1i-4.2)").Solve());
        Assert.AreEqual(0, new Equation("Int()").Solve());
    }

    [TestMethod]
    public void Frac()
    {
        Assert.AreEqual(BigRational.Parse(".2"), new Equation("Frac(2.2)").Solve());
        Assert.AreEqual(0, new Equation("Frac(2)").Solve());
        Assert.AreEqual(0, new Equation("Frac()").Solve());
        Assert.AreEqual(new BigComplex(0, BigRational.Parse("-.1")), new Equation("Frac(3i-3.1)").Solve());
    }

    [TestMethod]
    public void If()
    {
        Assert.AreEqual(0, new Equation("If()").Solve());
        Assert.AreEqual(true, new Equation("If(1=1)").Solve());
        Assert.AreEqual(2, new Equation("If(1=1, 2, 3)").Solve());
        Assert.AreEqual(3, new Equation("If(1=2, 2, 3)").Solve());
        Assert.AreEqual(2, new Equation("If(1=1, 2)").Solve());
        Assert.AreEqual(false, new Equation("If(1=2, 2)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("If(false, 2, -inf)").Solve());
    }

    [TestMethod]
    public void DataVal()
    {
        Assert.AreEqual(0, new Equation("DataVal()").Solve());
        Assert.AreEqual(new BigComplex(0, 1), new Equation("DataVal(0,1)").Solve());
        Assert.AreEqual(new BigComplex(0, 1), new Equation("DataVal(0,1, false)").Solve());
        Assert.IsTrue(new Equation("DataVal(1,0,true,0)").SolveBoolean());
        Assert.AreEqual(new BigComplex(0, 1), new Equation("DataVal(0,1, false, false)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("DataVal(1,0, false, true)").Solve());
    }

    [TestMethod]
    public void StringLength()
    {
        Assert.AreEqual(-1, new Equation("StringLength()").Solve());
        Assert.AreEqual(3, new Equation("StringLength(abc)").Solve());
        Assert.AreEqual(47, new Equation("StringLength(this was totally not a function to test strings)").Solve());
        Assert.AreEqual(3, new Equation("StringLength(a b)").Solve());
        Assert.AreEqual(0, new Equation("StringLength(,)").Solve());
    }

    [TestMethod]
    public void Sum()
    {
        Assert.AreEqual(15, new Equation("Sum(i = 0, 5, i)").Solve());
        Assert.AreEqual(6, new Equation("Sum(i = 0, 5, 1)").Solve());
        Assert.AreEqual(55, new Equation("Sum(i = 0, 5, i^2)").Solve());
        Assert.AreEqual(14, new Equation("Sum(i = e, 5, i)").Solve());
        Assert.AreEqual(5, new Equation("Sum(i = 5, 5, i)").Solve());
    }

    [TestMethod]
    public void Product()
    {
        Assert.AreEqual(0, new Equation("Product(i = 0, 5, i)").Solve());
        Assert.AreEqual(120, new Equation("Product(i = 1, 5, i)").Solve());
        Assert.AreEqual(64, new Equation("Product(i = 0, 5, 2)").Solve());
        Assert.AreEqual(14400, new Equation("Product(i = 1, 5, i^2)").Solve());
    }

    [TestMethod]
    public void Integral()
    {
        Assert.AreEqual(31, new Equation("Integral(0, 5, 1, x^2, x)-.25").Solve());
    }

    [TestMethod]
    public void Derivative()
    {
        Assert.AreEqual(2, new Equation("Derivative(x^2,0.01,1,x)-.01").Solve());
    }

    [TestMethod]
    public void For()
    {
        Assert.AreEqual(11, new Equation("For(let x = 0,x < 5,let x = x + 1,current+x,current,1)").Solve());
    }
}
