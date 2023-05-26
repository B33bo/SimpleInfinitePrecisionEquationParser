using System.Numerics;

namespace SIPEP.Tests;

[TestClass]
public class Solver
{
    [TestMethod]
    public void Basic()
    {
        var solutions = new Equation("x-10").FindSolutions("x", 20, 5, new BigRational(0.1), true);
        Assert.IsTrue(solutions.Length == 1);
        Assert.IsTrue(solutions[0].Real > 9.9 && solutions[0].Real < 10.1);
    }

    [TestMethod]
    public void Complex()
    {
        var solutions = new Equation("x-10-i").FindSolutions("x", 20, 5, new BigRational(0.1), false);
        Assert.IsTrue(solutions.Length == 1);
        Assert.IsTrue(solutions[0].Real > 9.9 && solutions[0].Real < 10.1);
        Assert.IsTrue(solutions[0].Imaginary > .9 && solutions[0].Imaginary < 1.1);
    }

    [TestMethod]
    public void TwoSolutions()
    {
        var solutions = new Equation("x^2-1").FindSolutions("x", 20, 5, new BigRational(0.1), true);
        Assert.IsTrue(solutions.Length == 2);

        if (solutions[0].Real > solutions[1].Real)
            (solutions[0], solutions[1]) = (solutions[1], solutions[0]); // bubble sort or insertion sort; you decide.

        Assert.IsTrue(solutions[0].Real > -1.1 && solutions[0].Real < -.9);
        Assert.IsTrue(solutions[1].Real > .9 && solutions[1].Real < 1.1);
    }
}
