using System.Numerics;

namespace SIPEP.Tests.Functions;
[TestClass]
public class Stats
{
    [TestMethod]
    public void Min()
    {
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Min(2)").Solve());
        Assert.AreEqual(0, new Equation("Min()").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Min(3, -inf, 5)").Solve());
    }

    [TestMethod]
    public void Max()
    {
        Assert.AreEqual(new BigComplex(14, 0), new Equation("Max(pi,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Max(2)").Solve());
        Assert.AreEqual(0, new Equation("Max()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Max(3, inf, 5)").Solve());
    }

    [TestMethod]
    public void Mean()
    {
        Assert.AreEqual(new BigComplex(BigRational.Parse("5"), 0), new Equation("Mean(2,5, 8)").Solve());
        Assert.AreEqual(new BigComplex(BigRational.Parse("7.125"), 0), new Equation("Mean(.5,12, 2, 14)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Mean(2)").Solve());
        Assert.AreEqual(0, new Equation("Mean()").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Mean(0, 2, inf)").Solve());
    }

    [TestMethod]
    public void GeometricMean()
    {
        Assert.AreEqual(3, new Equation("GeometricMean(3,3,3)").Solve());
        Assert.AreEqual(3, new Equation("GeometricMean(3,3)").Solve());
        Assert.AreEqual(4, new Equation("GeometricMean(2,8)").Solve());
        Assert.AreEqual(0, new Equation("GeometricMean()").Solve());
        Assert.AreEqual(2, new Equation("GeometricMean(2)").Solve());
    }

    [TestMethod]
    public void Median()
    {
        Assert.AreEqual(3, new Equation("Median(1,3,6000)").Solve());
        Assert.AreEqual(2, new Equation("Median(1,3)").Solve());
        Assert.AreEqual(5, new Equation("Median(5,2,6)").Solve());
        Assert.AreEqual(0, new Equation("Median()").Solve());
        Assert.AreEqual(3, new Equation("Median(3)").Solve());
    }

    [TestMethod]
    public void Mode()
    {
        Assert.AreEqual(2, new Equation("Mode(0,1,1,2,2,2,4,1,2)").Solve());
        Assert.AreEqual(1, new Equation("Mode(1)").Solve());
        Assert.AreEqual(0, new Equation("Mode()").Solve());
        Assert.AreEqual(3, new Equation("Mode(2,2,4,4)").Solve());
    }

    [TestMethod]
    public void Range()
    {
        Assert.AreEqual(5, new Equation("Range(2,5,1,0,4)").Solve());
        Assert.AreEqual(3, new Equation("Range(2,5)").Solve());
        Assert.AreEqual(0, new Equation("Range(2)").Solve());
        Assert.AreEqual(0, new Equation("Range()").Solve());
    }

    [TestMethod]
    public void Variance()
    {
        var arg1 = new Equation("Variance(2,3,2,1,3)").Solve().Real;
        var arg2 = new Equation("Variance(2,3,2,10,3)").Solve().Real;
        Assert.IsTrue(arg1 > .5 && arg1 < .6);
        Assert.IsTrue(arg2 > 9.1 && arg2 < 9.3);
        Assert.AreEqual(0, new Equation("Variance(2)").Solve());
        Assert.AreEqual(0, new Equation("Variance()").Solve());
    }

    [TestMethod]
    public void VarianceSample()
    {
        var arg1 = new Equation("VarianceSample(2,3,2,1,3)").Solve().Real;
        var arg2 = new Equation("VarianceSample(2,3,2,10,3)").Solve().Real;
        Assert.IsTrue(arg1 > .69 && arg1 < .71);
        Assert.IsTrue(arg2 > 11.49 && arg2 < 11.51);
        Assert.AreEqual(0, new Equation("VarianceSample(2)").Solve());
        Assert.AreEqual(0, new Equation("VarianceSample()").Solve());
    }

    [TestMethod]
    public void StandardDeviation()
    {
        var arg1 = new Equation("StandardDeviation(2,3,2,1,3)").Solve().Real;
        var arg2 = new Equation("StandardDeviation(2,3,2,10,3)").Solve().Real;
        Assert.IsTrue(arg1 > .74 && arg1 < .75);
        Assert.IsTrue(arg2 > 3 && arg2 < 3.1);
        Assert.AreEqual(0, new Equation("Variance(2)").Solve());
        Assert.AreEqual(0, new Equation("Variance()").Solve());
    }

    [TestMethod]
    public void StandardDeviationSample()
    {
        var arg1 = new Equation("StandardDeviationSample(2,3,2,1,3)").Solve().Real;
        var arg2 = new Equation("StandardDeviationSample(2,3,2,10,3)").Solve().Real;
        Assert.IsTrue(arg1 > .83 && arg1 < .84);
        Assert.IsTrue(arg2 > 3.3 && arg2 < 3.4);
        Assert.AreEqual(0, new Equation("StandardDeviationSample(2)").Solve());
        Assert.AreEqual(0, new Equation("StandardDeviationSample()").Solve());
    }
}
