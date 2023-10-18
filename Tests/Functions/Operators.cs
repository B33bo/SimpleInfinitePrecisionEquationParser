using System.Numerics;

namespace SIPEP.Tests.Functions;

[TestClass]
public class Operators
{
    [TestMethod]
    public void Add()
    {
        Assert.AreEqual(new BigComplex(4, 0), new Equation("1+3").Solve());
        Assert.AreEqual(new BigComplex(4, 0), new Equation("Add(1,3)").Solve());
        Assert.AreEqual(new BigComplex(3, 6), new Equation("Add(0i1,3i5)").Solve());
        Assert.AreEqual(new BigComplex(8, 1), new Equation("Add(3,0i1,5)").Solve());
        Assert.AreEqual(new BigComplex(3, 0), new Equation("Add(3)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Add()").Solve());

        Assert.AreEqual(-BigComplex.Infinity, new Equation("Add(3,-inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Add(3,inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Add(-inf,-inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Add(inf,inf)").Solve());
    }

    [TestMethod]
    public void Subtract()
    {
        Assert.AreEqual(new BigComplex(-1, 0), new Equation("1-2").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Subtract(1,1)").Solve());
        Assert.AreEqual(new BigComplex(1, -1), new Equation("Subtract(1,0i1)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Subtract(5,3,2)").Solve());
        Assert.AreEqual(new BigComplex(-3, 0), new Equation("Subtract(3)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Subtract()").Solve());

        Assert.AreEqual(BigComplex.Infinity, new Equation("Subtract(3,-inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Subtract(3,inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Subtract(-inf,-inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Subtract(inf,inf)").Solve());
    }

    [TestMethod]
    public void Multiply()
    {
        Assert.AreEqual(new BigComplex(12, 0), new Equation("4*3").Solve());
        Assert.AreEqual(new BigComplex(120, 0), new Equation("Multiply(40,3)").Solve());
        Assert.AreEqual(new BigComplex(-5, 3), new Equation("Multiply(0i1,3i5)").Solve());
        Assert.AreEqual(new BigComplex(24, 0), new Equation("Multiply(2,3,4)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Multiply(2)").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Multiply()").Solve());

        Assert.AreEqual(BigComplex.Infinity, new Equation("Multiply(2, inf)").Solve());
        Assert.AreEqual(0, new Equation("Multiply(0, inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Multiply(2, -inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Multiply(inf, inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Multiply(inf, -inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Multiply(-inf, -inf)").Solve());
    }

    [TestMethod]
    public void MultiplyOperator()
    {
        Assert.AreEqual(new BigComplex(80, 0), new Equation("MultiplyOperator(40, 2)").Solve());

        Assert.AreEqual(new Equation("MultiplyOperator(5)").Solve(), (BigComplex)5);
        Assert.AreEqual(new Equation("MultiplyOperator(2+3*i)").Solve(), new BigComplex(2, -3));

        Assert.AreEqual(new BigComplex(1, 0), new Equation("MultiplyOperator()").Solve());
        Assert.AreEqual(new BigComplex(6, 0), new Equation("MultiplyOperator(1, 2, 3)").Solve());
    }

    [TestMethod]
    public void Conjugate()
    {
        Assert.AreEqual(new BigComplex(40, 0), new Equation("Conjugate(40)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Conjugate(2,3)").Solve());
        Assert.AreEqual(new BigComplex(3, -1), new Equation("Conjugate(3i1)").Solve());
        Assert.AreEqual(new BigComplex(2, 3), new Equation("Conjugate(2 - (3*i))").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Conjugate()").Solve());
    }

    [TestMethod]
    public void ToDegrees()
    {
        var c = new Equation("ToDegrees(pi)").Solve();

        Assert.IsTrue(c.Real > 179 && c.Real < 181);
        Assert.AreEqual(new BigComplex(0, 0), new Equation("ToDegrees()").Solve());
    }

    [TestMethod]
    public void Divide()
    {
        Assert.AreEqual(BigComplex.Infinity, new Equation("Divide(inf, 3)").Solve());
        Assert.AreEqual(new BigComplex(BigRational.Parse(".5"), 0), new Equation("1/2").Solve());
        Assert.AreEqual(new BigComplex(1, 0), new Equation("Divide(1,1)").Solve());
        Assert.AreEqual(new BigComplex(0, -5), new Equation("Divide(5,0i1)").Solve());
        Assert.AreEqual(new BigComplex(2, 0), new Equation("Divide(2)").Solve());
        Assert.AreEqual(new BigComplex(0, 0), new Equation("Divide()").Solve());
        Assert.AreEqual(0, new Equation("Divide(5, inf)").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("Divide(inf, inf)").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("Divide(-inf, 2)").Solve());
    }

    [TestMethod]
    public void Pow()
    {
        Assert.AreEqual(new BigComplex(-16, 30), new Equation("pow(3i5, 2)").Solve());
        Assert.AreEqual(1, new Equation("pow()").Solve().Real);
        Assert.AreEqual(64, new Equation("pow(2,2,3)").Solve().Real);
        Assert.AreEqual(-1, new Equation("pow(0i1,2)").Solve().Real);
        Assert.AreEqual(64, new Equation("4^3").Solve().Real);
        Assert.AreEqual(25, new Equation("5^2").Solve());
        Assert.AreEqual(8, new Equation("pow(2,3)").Solve().Real);
        Assert.AreEqual(BigComplex.Infinity, new Equation("inf^2").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("inf^2.1").Solve());
        Assert.AreEqual(-BigComplex.Infinity, new Equation("-inf^2.1").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("2^inf").Solve());

        var pow = new Equation("pow(2, 2.5)").Solve(); //-16 30
        Assert.IsTrue(pow.Real > 5.6 && pow.Real < 5.7);
    }

    [TestMethod]
    public void Root()
    {
        var root1 = new Equation(@"2\25").Solve().Real;
        var root2 = new Equation(@"Root(2,1)").Solve().Real;
        var root3 = new Equation(@"Root(2,-1)").Solve().Imaginary;
        var root4 = new Equation(@"Root(2,2,2)").Solve().Real;
        var root5 = new Equation(@"Root(2)").Solve().Real;
        var root6 = new Equation(@"Root()").Solve().Real;

        Assert.IsTrue(root1 > 4.9 && root1 < 5.1);
        Assert.IsTrue(root2 > .9 && root2 < 1.1);
        Assert.IsTrue(root3 > .9 && root3 < 1.1);
        Assert.IsTrue(root4 > 1.4 && root4 < 1.5);
        Assert.IsTrue(root5 > 1.4 && root5 < 1.5);
        Assert.IsTrue(root6 > -.1 && root6 < .1);

        Assert.AreEqual(BigComplex.Infinity, new Equation("Root(inf)").Solve());
        Assert.AreEqual(1, new Equation("Root(inf, 2)").Solve());
    }

    [TestMethod]
    public void Mod()
    {
        Assert.AreEqual((BigComplex)2, new Equation("11 % 3").Solve());
        Assert.AreEqual((BigComplex)2, new Equation("-10 % 3").Solve());
        Assert.AreEqual(0, new Equation("inf % 3").Solve());
        Assert.AreEqual(2, new Equation("2 % inf").Solve());
    }

    [TestMethod]
    public void Factorial()
    {
        Assert.AreEqual(0, new Equation("Factorial()").Solve());
        Assert.AreEqual(1, new Equation("0!").Solve());
        Assert.AreEqual(120, new Equation("5!").Solve());
        Assert.AreEqual(BigComplex.Infinity, new Equation("inf!").Solve());

        var fact1 = new Equation("Factorial(0.5,10)").Solve().Real;
        var fact2 = new Equation("Factorial(-0.5,10)").Solve().Real;
        var fact3 = new Equation("Factorial(i,10)").Solve();

        Assert.IsTrue(fact1 > 0.8 && fact1 < 0.9);
        Assert.IsTrue(fact2 > 1.7 && fact2 < 1.9);
        Assert.IsTrue(fact3.Real > .4 && fact3.Real < .52 && fact3.Imaginary > -.2 && fact3.Imaginary < -.1);
    }
}
