using System.Numerics;

namespace SIPEP.Tests;

[TestClass]
public class EquationTests
{
    [TestMethod]
    public void Basic()
    {
        Assert.AreEqual(new BigComplex(2, 0), new Equation("1+    1").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Add(1,1)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("3,-2").Solve());
    }

    [TestMethod]
    public void Nesting()
    {
        Assert.AreEqual(new BigComplex(7, 0), new Equation("(5)+(1+(1+1)/2)").Solve()); // 5 + (1 + 2 / 2) -> 5 + (1 + 1) -> 7
        Assert.AreEqual(new BigComplex(15, 0), new Equation("Add(1, (1 + Add(1,1)), 3) + 8").Solve());
    }

    [TestMethod]
    public void VariableTests()
    {
        Assert.AreEqual(new BigComplex(0, 2), new Equation("i+i").Solve());
        Assert.AreEqual(new BigComplex(0, 1), new Equation("i").Solve());
        Assert.AreEqual(new BigComplex(25, 0), new Equation("num*num", new Dictionary<string, Variable>() { { "num", new BigComplex(5, 0)}  }).Solve());
    }

    [TestMethod]
    public void Negative()
    {
        Assert.AreEqual(new BigComplex(-2, 0), new Equation("-2").Solve());
        Assert.AreEqual(new BigComplex(-2, 0), new Equation("-(2)").Solve());
    }

    [TestMethod]
    public void VarAssignment()
    {
        var e = new Equation("let x = 3");
        Assert.AreEqual(new BigComplex(3, 0), e.Solve());
        e.Parse("let y = x + 1");
        Assert.AreEqual(new BigComplex(4, 0), e.Solve());
        e.Parse("let f(q) = y + q");
        e.Solve();
        e.Parse("f(4)");
        Assert.AreEqual(new BigComplex(8, 0), e.Solve());
    }

    [TestMethod]
    public void Reusability()
    {
        Equation e = new("let x = 5");
        e.Solve();
        e.Parse("x + 5");
        Assert.AreEqual(new BigComplex(10, 0), e.Solve());
        e.Variables["x"] = (BigComplex)3;
        Assert.AreEqual(new BigComplex(8, 0), e.Solve());
    }

    [TestMethod]
    public void StringCheck()
    {
        Equation e = new("stringlength(f(3,2), 3)");
        Assert.AreEqual(6, e.Solve());
    }

    [TestMethod]
    public void Const()
    {
        Assert.IsTrue(new Equation("1+1").IsConstant);
        Assert.IsTrue(new Equation("1+pi").IsConstant);
        Assert.IsTrue(new Equation("1+(1+1)").IsConstant);
        Assert.IsFalse(new Equation("1+(1+x)").IsConstant);
        Assert.IsFalse(new Equation("x+(1+x)").IsConstant);
    }

    [TestMethod]
    public void Simplify()
    {
        var eq = new Equation("(1+1)+x");
        eq.Simplify();
        Assert.AreEqual((BigComplex)2, eq.Data[0].data);

        eq.Parse("(5 * 2)+x");
        eq.Simplify();
        Assert.AreEqual((BigComplex)10, eq.Data[0].data);

        eq.Parse("deka+x");
        eq.Simplify();
        Assert.AreEqual((BigComplex)10, eq.Data[0].data);

        eq.Parse("(2*2)+4+x");
        eq.Simplify();
        Assert.AreEqual((BigComplex)4, eq.Data[0].data);

        eq.Parse("(2*deka)+4+x");
        eq.Simplify();
        Assert.AreEqual((BigComplex)20, eq.Data[0].data);

        eq.Parse("(2*x)+4+x");
        eq.Simplify();
        Assert.AreEqual(Equation.SectionType.NestedEquation, eq.Data[0].type);
    }

    [TestMethod]
    public void Capitilisation()
    {
        Assert.AreEqual(new Equation("ConvertTemperature()").Solve(), new Equation("convertTemperature()").Solve());
    }
}