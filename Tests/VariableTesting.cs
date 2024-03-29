﻿namespace SIPEP.Tests;

[TestClass]
public class VariableTesting
{
    [TestMethod]
    public void Vars()
    {
        Equation e = new("let x = 1");
        Assert.AreEqual(new BigComplex(1, 0), e.Solve());
        e.Parse("x + 1");
        Assert.AreEqual(new BigComplex(2, 0), e.Solve());
    }

    [TestMethod]
    public void Functions()
    {
        Equation e = new Equation("let func(x, y) = x * y - 2");
        Assert.IsTrue(e.SolveBoolean());
        e.Parse("func(10, 2)");
        Assert.AreEqual(new BigComplex(18, 0), e.Solve());
        e.Parse("let funcTwo(x) = func(x, x) + 1");
        Assert.AreEqual(new BigComplex(true), e.Solve());
        e.Parse("funcTwo(5)");
        Assert.AreEqual(new BigComplex(24, 0), e.Solve());
        e.Parse("func(func(2, 3), 2)");
        Assert.AreEqual(new BigComplex(6, 0), e.Solve());
        e.Parse("1 + (let x = 5)");
        Assert.AreEqual(new BigComplex(6, 0), e.Solve());

        //prime finder equation. Very slow but computes the nth prime https://www.youtube.com/watch?v=j5s0h42GfvM
        e.Parse("let prime(n) = 1 + sum(i = 1, 2^n, floor((n/ sum(j = 1, i, floor(cos(pi * (((j-1)!)+1)/j)^2))) ^ (1/n)))");
        Assert.IsTrue(e.SolveBoolean());
        e.Parse("prime(1)");
        Assert.AreEqual(2, e.Solve());
    }
}
