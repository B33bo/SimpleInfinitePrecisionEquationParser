using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPEP.Tests.Functions;

[TestClass]
public class Boolean
{
    [TestMethod]
    public void And()
    {
        Assert.IsFalse(new Equation("false & false").SolveBoolean());
        Assert.IsFalse(new Equation("false & true").SolveBoolean());
        Assert.IsFalse(new Equation("true & false").SolveBoolean());
        Assert.IsTrue(new Equation("true & true").SolveBoolean());

        Assert.IsTrue(new Equation("And(true,true)").SolveBoolean());
        Assert.IsTrue(new Equation("And(true, true, true)").SolveBoolean());
        Assert.IsFalse(new Equation("And(true, true, false)").SolveBoolean());
        Assert.IsTrue(new Equation("And()").SolveBoolean());
    }

    [TestMethod]
    public void Or()
    {
        Assert.IsFalse(new Equation("false | false").SolveBoolean());
        Assert.IsTrue(new Equation("false | true").SolveBoolean());
        Assert.IsTrue(new Equation("true | false").SolveBoolean());
        Assert.IsTrue(new Equation("true | true").SolveBoolean());

        Assert.IsTrue(new Equation("Or(true,true)").SolveBoolean());
        Assert.IsTrue(new Equation("Or(true, true, true)").SolveBoolean());
        Assert.IsTrue(new Equation("Or(true, true, false)").SolveBoolean());
        Assert.IsFalse(new Equation("Or()").SolveBoolean());
    }

    [TestMethod]
    public void Xor()
    {
        Assert.IsFalse(new Equation("Xor(false, false)").SolveBoolean());
        Assert.IsTrue(new Equation("Xor(false, true)").SolveBoolean());
        Assert.IsTrue(new Equation("Xor(true, false)").SolveBoolean());
        Assert.IsFalse(new Equation("Xor(true, true)").SolveBoolean());

        Assert.IsFalse(new Equation("Xor(true, true, false)").SolveBoolean());

        Assert.IsFalse(new Equation("Xor()").SolveBoolean());
    }

    [TestMethod]
    public void Not()
    {
        Assert.IsTrue(new Equation("Not(false)").SolveBoolean());
        Assert.IsFalse(new Equation("Not(true)").SolveBoolean());
        Assert.IsTrue(new Equation("Not()").SolveBoolean());

        Assert.IsFalse(new Equation("¬(true)").SolveBoolean());
        Assert.IsFalse(new Equation("¬true").SolveBoolean());
        Assert.IsTrue(new Equation("¬false").SolveBoolean());
    }

    [TestMethod]
    public void Equals()
    {
        Assert.IsTrue(new Equation("4 = 8 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 = 3").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 = 3").SolveBoolean());

        Assert.IsFalse(new Equation("Equals(1,2)").SolveBoolean());
        Assert.IsTrue(new Equation("Equals(1)").SolveBoolean());
        Assert.IsTrue(new Equation("Equals()").SolveBoolean());
    }

    [TestMethod]
    public void Greater()
    {
        Assert.IsTrue(new Equation("4.1 > 8 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 > 2").SolveBoolean());

        Assert.IsFalse(new Equation("Greater(1,2)").SolveBoolean());
        Assert.IsTrue(new Equation("Greater(2, 1, 0)").SolveBoolean());
        Assert.IsFalse(new Equation("Greater(1)").SolveBoolean());
        Assert.IsFalse(new Equation("Greater()").SolveBoolean());
    }

    [TestMethod]
    public void Less()
    {
        Assert.IsTrue(new Equation("4.1 < 9 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 < 2").SolveBoolean());

        Assert.IsFalse(new Equation("Less(2,1)").SolveBoolean());
        Assert.IsTrue(new Equation("Less(4, 6, 8)").SolveBoolean());
        Assert.IsFalse(new Equation("Less(1)").SolveBoolean());
        Assert.IsFalse(new Equation("Less()").SolveBoolean());
    }
}
