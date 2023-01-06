using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SIPEP.Tests;

[TestClass]
public class VariableTesting
{
    [TestMethod]
    public void Vars()
    {
        Equation e = new Equation("let x = 1");
        Assert.AreEqual(new BigComplex(1, 0), e.Solve());
        e.LoadString("x + 1");
        Assert.AreEqual(new BigComplex(2, 0), e.Solve());
    }

    [TestMethod]
    public void Functions()
    {
        Equation e = new Equation("let func(x, y) = x * y - 2");
        Assert.AreEqual(new BigComplex(true), e.Solve());
        e.LoadString("func(10, 2)");
        Assert.AreEqual(new BigComplex(18, 0), e.Solve());
        e.LoadString("let funcTwo(x) = func(x, x) + 1");
        Assert.AreEqual(new BigComplex(true), e.Solve());
        e.LoadString("funcTwo(5)");
        Assert.AreEqual(new BigComplex(24, 0), e.Solve());
        e.LoadString("func(func(2, 3), 2)");
        Assert.AreEqual(new BigComplex(6, 0), e.Solve());
        e.LoadString("1 + (let x = 5)");
        Assert.AreEqual(new BigComplex(6, 0), e.Solve());
    }
}
