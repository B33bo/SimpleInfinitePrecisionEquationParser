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

        Assert.IsTrue(new Equation("inf=inf").SolveBoolean());
        Assert.IsFalse(new Equation("inf=-inf").SolveBoolean());
        Assert.IsFalse(new Equation("inf=1").SolveBoolean());
    }

    [TestMethod]
    public void Greater()
    {
        Assert.IsTrue(new Equation("4.1 > 8 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 > 2").SolveBoolean());
        Assert.IsTrue(new Equation("5 > -6").SolveBoolean());
        Assert.IsTrue(new Equation("0 > -0.2").SolveBoolean());

        Assert.IsFalse(new Equation("Greater(1,2)").SolveBoolean());
        Assert.IsTrue(new Equation("Greater(2, 1, 0)").SolveBoolean());
        Assert.IsFalse(new Equation("Greater(1)").SolveBoolean());
        Assert.IsFalse(new Equation("Greater()").SolveBoolean());

        Assert.IsTrue(new Equation("Greater(inf, 1, 0, -inf)").SolveBoolean());
        Assert.IsFalse(new Equation("-inf > 1").SolveBoolean());
        Assert.IsTrue(new Equation("inf > inf").SolveBoolean());
        Assert.IsFalse(new Equation("-inf > inf").SolveBoolean());
    }

    [TestMethod]
    public void Less()
    {
        Assert.IsTrue(new Equation("4.1 < 9 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 < 2").SolveBoolean());

        Assert.IsFalse(new Equation("5 < -6").SolveBoolean());
        Assert.IsFalse(new Equation("0 < -0.2").SolveBoolean());

        Assert.IsFalse(new Equation("Less(2,1)").SolveBoolean());
        Assert.IsTrue(new Equation("Less(4, 6, 8)").SolveBoolean());
        Assert.IsFalse(new Equation("Less(1)").SolveBoolean());
        Assert.IsFalse(new Equation("Less()").SolveBoolean());

        Assert.IsTrue(new Equation("Less(-inf, 1, 2, inf)").SolveBoolean());
        Assert.IsTrue(new Equation("-inf < 1").SolveBoolean());
        Assert.IsFalse(new Equation("inf < inf").SolveBoolean());
        Assert.IsFalse(new Equation("inf < -inf").SolveBoolean());
    }

    [TestMethod]
    public void Approx()
    {
        Assert.IsTrue(new Equation("4 ~ 8 / 2").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 ~ 3").SolveBoolean());
        Assert.IsFalse(new Equation("1 + 1 ~ 3").SolveBoolean());

        Assert.IsFalse(new Equation("Approx(1,2)").SolveBoolean());
        Assert.IsTrue(new Equation("Approx(0.1, 0.12)").SolveBoolean());
        Assert.IsFalse(new Equation("Approx(0.1, 0.12, 0.003)").SolveBoolean());
        Assert.IsTrue(new Equation("Approx(1)").SolveBoolean());
        Assert.IsTrue(new Equation("Approx()").SolveBoolean());

        Assert.IsTrue(new Equation("inf~inf").SolveBoolean());
        Assert.IsFalse(new Equation("inf~1").SolveBoolean());
        Assert.IsFalse(new Equation("inf~-inf").SolveBoolean());
        Assert.IsFalse(new Equation("inf~1").SolveBoolean());
    }

    [TestMethod]
    public void Domain()
    {
        Assert.IsTrue(new Equation("domain(1,*)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(inf,INF)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(3,#)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(true,BOOL)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(false,B)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(3,BOOL)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(NaN,NAN)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(NaN,?)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,Natural)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1,Natural)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(-1,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(2.5,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(i,N)").SolveBoolean());

        Assert.IsFalse(new Equation("domain(0,N1)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1,Natural-1)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(-1,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(2.5,N)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(i,N)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,Z)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1,Integer)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2,Z)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(-1,Z)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(2.5,Z)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(i,Z)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,Q)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1/3,Rational)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(pi,Q)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(-1,Q)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2.5,Q)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(i,Q)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,R)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1,Real)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2,R)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(-1,R)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2.5,R)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(i,R)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,I)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(1,Imaginary)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(2,I)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(3*i,I)").SolveBoolean());
        Assert.IsFalse(new Equation("domain(3*i + 2,I)").SolveBoolean());

        Assert.IsTrue(new Equation("domain(0,C)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(1,Complex)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2,C)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(-1,C)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(2.5,C)").SolveBoolean());
        Assert.IsTrue(new Equation("domain(i,C)").SolveBoolean());

        Assert.IsFalse(new Equation("domain(i,P)").SolveBoolean());
    }
}
