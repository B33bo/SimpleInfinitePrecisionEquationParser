using System.Numerics;

namespace SIPEP.Tests;

[TestClass]
public class EquationTests
{
    [TestMethod]
    public void Basic()
    {
        Assert.AreEqual(new BigComplex(2, 0), new Equation("1+    1").Solve());
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
        Assert.AreEqual(new BigComplex(2, 0), new Equation("one+one").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("one").Solve());
        Assert.AreEqual(new BigComplex(25, 0), new Equation("num*num", new Dictionary<string, BigComplex>() { { "num", new BigComplex(5, 0)}  }).Solve());
    }
}