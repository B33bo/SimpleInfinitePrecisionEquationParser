using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Tests.Functions;

[TestClass]
public class Trig
{
    #region Trig

    [TestMethod]
    public void Sin()
    {
        var num1 = new Equation("sin(pi)").Solve().Real;
        var num2 = new Equation("sin(pi/2)").Solve().Real;
        var num3 = new Equation("sin(5)").Solve().Real;
        var num4 = new Equation("sin(3i5)").Solve();
        var num5 = new Equation("sin(-pi / 2)").Solve().Real;

        Assert.AreEqual(new BigComplex(0, 0), new Equation("sin()").Solve());

        Assert.IsTrue(num1 > -.1 && num1 < .1); //0
        Assert.IsTrue(num2 > .9 && num2 < 1.1); //1
        Assert.IsTrue(num3 > -1 && num3 < -.8); // .95...
        Assert.IsTrue(num4.Real > 10.4 && num4.Real < 10.5 && num4.Imaginary > -73.5 && num4.Imaginary < 73.4);
        Assert.IsTrue(num5 > -1.1 && num5 < -.9);
    }

    [TestMethod]
    public void Sinh()
    {
        var num1 = new Equation("sinh(pi)").Solve().Real;
        var num2 = new Equation("sinh(pi/2)").Solve().Real;
        var num3 = new Equation("sinh(5)").Solve().Real;
        var num4 = new Equation("sinh(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("sinh()").Solve());

        Assert.IsTrue(num1 > 11.5 && num1 < 11.6);
        Assert.IsTrue(num2 > 2.3 && num2 < 2.4);
        Assert.IsTrue(num3 > 74.2 && num3 < 74.3);
        Assert.IsTrue(num4.Real > 2.8 && num4.Real < 2.9 && num4.Imaginary > -9.7 && num4.Imaginary < -9.5);
    }

    [TestMethod]
    public void Asin()
    {
        var num1 = new Equation("asin(0)").Solve().Real;
        var num2 = new Equation("asin(.1)").Solve().Real;
        var num3 = new Equation("asin(10)").Solve();
        var num4 = new Equation("asin(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("asin()").Solve());

        Assert.IsTrue(num1 > -.1 && num1 < .1);
        Assert.IsTrue(num2 > .1 && num2 < .2);
        Assert.IsTrue(num3.Real > 1.5 && num3.Real < 1.6 && num3.Imaginary > 2.9 && num3.Imaginary < 3.1);
        Assert.IsTrue(num4.Real > .5 && num4.Real < .6 && num4.Imaginary > 2.4 && num4.Imaginary < 2.5);
    }

    [TestMethod]
    public void Cos()
    {
        var num1 = new Equation("cos(pi)").Solve().Real;
        var num2 = new Equation("cos(pi/2)").Solve().Real;
        var num3 = new Equation("cos(5)").Solve().Real;
        var num4 = new Equation("cos(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("cos()").Solve());
        Assert.AreEqual(new Equation("cos(pi)").Solve(), new Equation("cos(-pi)").Solve());

        Assert.IsTrue(num1 > -1.1 && num1 < .9);
        Assert.IsTrue(num2 > -.1 && num2 < .1);
        Assert.IsTrue(num3 > .2 && num3 < .3);
        Assert.IsTrue(num4.Real > -73.5 && num4.Real < -73.4 && num4.Imaginary > -10.5 && num4.Imaginary < -10.4);
    }

    [TestMethod]
    public void Cosh()
    {
        var num1 = new Equation("cosh(pi)").Solve().Real;
        var num2 = new Equation("cosh(pi/2)").Solve().Real;
        var num3 = new Equation("cosh(5)").Solve().Real;
        var num4 = new Equation("cosh(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("cosh()").Solve());

        Assert.IsTrue(num1 > -11.5 && num1 < 11.6);
        Assert.IsTrue(num2 > 2.5 && num2 < 2.6);
        Assert.IsTrue(num3 > 74.1 && num3 < 74.3);
        Assert.IsTrue(num4.Real > 2.8 && num4.Real < 2.9 && num4.Imaginary > -9.7 && num4.Imaginary < -9.5);
    }

    [TestMethod]
    public void Acos()
    {
        var num1 = new Equation("acos(0)").Solve().Real;
        var num2 = new Equation("acos(.1)").Solve().Real;
        var num3 = new Equation("acos(10)").Solve();
        var num4 = new Equation("acos(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("acos()").Solve());

        Assert.IsTrue(num1 > 1.5 && num1 < 1.6);
        Assert.IsTrue(num2 > 1.4 && num2 < 1.5);
        Assert.IsTrue(num3.Real > -.1 && num3.Real < .1 && num3.Imaginary > 2.8 && num3.Imaginary < 3.1);
        Assert.IsTrue(num4.Real > 1 && num4.Real < 1.1 && num4.Imaginary > -2.5 && num4.Imaginary < -2.3);
    }

    [TestMethod]
    public void Tan()
    {
        var num1 = new Equation("tan(pi)").Solve().Real;
        var num2 = new Equation("tan(5)").Solve().Real;
        var num3 = new Equation("tan(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("tan()").Solve());

        Assert.IsTrue(num1 > -.1 && num1 < .1);
        Assert.IsTrue(num2 > -3.4 && num2 < -3.2);
        Assert.IsTrue(num3.Real > -.1 && num3.Real < .1 && num3.Imaginary > .9 && num3.Imaginary < 1.1);
    }

    [TestMethod]
    public void Tanh()
    {
        var num1 = new Equation("tanh(pi)").Solve().Real;
        var num2 = new Equation("tanh(pi/2)").Solve().Real;
        var num3 = new Equation("tanh(5)").Solve().Real;

        Assert.AreEqual(new BigComplex(0, 0), new Equation("tanh()").Solve());

        Assert.IsTrue(num1 > .9 && num1 < 1.1);
        Assert.IsTrue(num2 > .9 && num2 < 1);
        Assert.IsTrue(num3 > .9 && num3 < 1.1);
    }

    [TestMethod]
    public void Atan()
    {
        var num1 = new Equation("atan(0)").Solve().Real;
        var num2 = new Equation("atan(.1)").Solve().Real;
        var num3 = new Equation("atan(10)").Solve().Real;
        var num4 = new Equation("atan(3i5)").Solve();

        Assert.AreEqual(new BigComplex(0, 0), new Equation("atan()").Solve());

        Assert.IsTrue(num1 > -.1 && num1 < .1);
        Assert.IsTrue(num2 > 0 && num2 < .2);
        Assert.IsTrue(num3 > 1.4 && num3 < 1.5);
        Assert.IsTrue(num4.Real > 1.4 && num4.Real < 1.5 && num4.Imaginary > .1 && num4.Imaginary < .2);
    }

    #endregion
}
