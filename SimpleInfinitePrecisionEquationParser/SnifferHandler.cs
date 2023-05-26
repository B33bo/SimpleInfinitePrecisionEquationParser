using SIPEP.Functions;
using System.Collections.Concurrent;
using System.Numerics;

namespace SIPEP;

internal class SnifferHandler
{
    public bool isDone;
    public BigComplex[] Output;
    private ConcurrentBag<BigComplex> result;
    private ConcurrentBag<BigComplex> roundedResult;
    private int threadsRemaining;
    private int depth;
    private BigRational cutoff;
    private string variable;
    private double MaxSearch;
    private Equation equation;
    private bool realOnly;

    public SnifferHandler(double max, Equation equation, string variable, int depth, BigRational cutoff, bool realOnly)
    {
        MaxSearch = max;
        this.variable = variable;
        this.equation = equation;
        this.depth = depth;
        this.cutoff = cutoff;
        result = new();
        roundedResult = new();
        this.realOnly = realOnly;

        if (realOnly)
        {
            threadsRemaining = 2;
            new Thread(() => { SearchLinear(1, 0, new Equation(equation)); threadsRemaining--; }).Start();
            new Thread(() => { SearchLinear(-1, 0, new Equation(equation)); threadsRemaining--; }).Start();
        }
        else
        {
            threadsRemaining = 4;
            new Thread(() => { SearchCorner(false, false); threadsRemaining--; }).Start();
            new Thread(() => { SearchCorner(false, true); threadsRemaining--; }).Start();
            new Thread(() => { SearchCorner(true, false); threadsRemaining--; }).Start();
            new Thread(() => { SearchCorner(true, true); threadsRemaining--; }).Start();
        }

        while (threadsRemaining != 0) ;

        TryValue(BigComplex.Infinity);
        TryValue(-BigComplex.Infinity);

        RemoveDuplicates();

        isDone = true;
    }

    private void TryValue(BigComplex value)
    {
        equation.SetVariable(variable, value);

        if (equation.Solve() == 0)
        {
            result.Add(value);
            roundedResult.Add(value);
        }
    }

    private void RemoveDuplicates()
    {
        List<BigComplex> dupeChecker = new(result.Count);
        List<BigComplex> values = new(result.Count);

        for (int i = 0; i < roundedResult.Count; i++)
        {
            var element = roundedResult.ElementAt(i);

            if (dupeChecker.Contains(element))
                continue;

            dupeChecker.Add(element);
            values.Add(result.ElementAt(i));
        }

        Output = values.ToArray();
    }

    public void SearchLinear(BigComplex direction, BigComplex diff, Equation equation)
    {
        bool posX = direction.Real >= 0;
        bool posY = diff.Imaginary >= 0;

        equation.SetVariable(variable, direction + diff);

        var lastValueDistTo0 = DistTo0(equation.Solve());

        bool isGoingDown = false;

        for (int i = 0; i <= MaxSearch; i++)
        {
            var valueBeingChecked = direction * i + diff;
            equation.SetVariable(variable, valueBeingChecked);
            var currentValue = equation.Solve();
            var currentValueDistTo0 = DistTo0(currentValue);

            if (currentValueDistTo0 < lastValueDistTo0)
            {
                isGoingDown = true;
            }
            else if (isGoingDown)
            {
                // current difference is higher when it shouldn't be
                BigComplex? val;
                if (realOnly)
                    val = SniffReal(direction * (i - 1) + diff, 1, depth, equation);
                else
                    val = Sniff(direction * (i - 1) + diff, 1, depth, equation, posX, posY);

                if (val != null)
                {
                    var roundedVal = Round(val.Value);
                    if (!roundedResult.Contains(roundedVal))
                    {
                        result.Add(val.Value);
                        roundedResult.Add(roundedVal);
                    }
                }

                isGoingDown = false;
            }

            lastValueDistTo0 = DistTo0(currentValue);
        }
    }

    private BigComplex Round(BigComplex complex)
    {
        return Misc.Round(complex, cutoff);
    }

    public void SearchCorner(bool posX, bool posI)
    {
        Equation eq = new Equation(equation);
        for (int i = 0; i <= MaxSearch; i++)
        {
            var difference = new BigComplex(0, i);

            if (!posI)
                difference *= -1;

            SearchLinear(posX ? BigComplex.One : -BigComplex.One, difference, eq);
        }
    }

    public BigComplex? SniffReal(BigComplex center, BigRational area, int depth, Equation eq)
    {
        BigComplex left = center - area;
        BigComplex slightLeft = center - (area / 2);
        BigComplex right = center + area;
        BigComplex slightRight = center + (area / 2);

        BigComplex[] allNumbers = new BigComplex[]
        {
            center,
            left, slightLeft,
            right, slightRight,
        };

        GetLowestValue(center, eq, allNumbers, out byte lowestIndex, out BigRational lowestValue);

        if (depth <= 0)
        {
            if (lowestValue > cutoff)
                return null;
            return allNumbers[lowestIndex];
        }

        return SniffReal(allNumbers[lowestIndex], area / 2, depth - 1, eq);
    }

    public BigComplex? Sniff(BigComplex center, BigRational area, int depth, Equation eq, bool posX, bool posY)
    {
        BigComplex bottomLeft = ClampToBound(new(center.Real - area, center.Imaginary - area), posX, posY);
        BigComplex topRight = ClampToBound(new(center.Real + area, center.Imaginary + area), posX, posY);

        BigComplex topLeft = ClampToBound(new(bottomLeft.Real, topRight.Imaginary), posX, posY);
        BigComplex bottomRight = ClampToBound(new(topRight.Real, bottomLeft.Imaginary), posX, posY);

        BigComplex centerLeft = ClampToBound(new(bottomLeft.Real, center.Imaginary), posX, posY);
        BigComplex centerRight = ClampToBound(new(topRight.Real, center.Imaginary), posX, posY);

        BigComplex topMiddle = ClampToBound(new(center.Real, topRight.Imaginary), posX, posY);
        BigComplex bottomMiddle = ClampToBound(new(center.Real, bottomRight.Imaginary), posX, posY);

        // now find the lowest of all of these numbers when plugged in
        BigComplex[] allNumbers = new BigComplex[]
        {
            center,
            bottomLeft, bottomRight, topLeft, topRight,
            centerLeft, centerRight, topMiddle, bottomMiddle,
        };
        GetLowestValue(center, eq, allNumbers, out byte lowestIndex, out BigRational lowestValue);

        // ok now you have the lowest one of all of these. Now reiterate it unless max depth reached

        if (depth <= 0)
        {
            if (lowestValue > cutoff)
                return null;
            return allNumbers[lowestIndex];
        }

        return Sniff(allNumbers[lowestIndex], area / 4, depth - 1, eq, posX, posY);
    }

    private void GetLowestValue(BigComplex center, Equation eq, BigComplex[] allNumbers, out byte lowestIndex, out BigRational lowestValue)
    {
        lowestIndex = 0;
        eq.SetVariable(variable, center);
        lowestValue = DistTo0(eq.Solve());
        for (byte i = 1; i < allNumbers.Length; i++)
        {
            eq.SetVariable(variable, allNumbers[i]);
            var currentSol = eq.Solve();

            if (DistTo0(currentSol) > lowestValue)
                continue;

            lowestValue = DistTo0(currentSol);
            lowestIndex = i;
        }
    }

    private static BigComplex ClampToBound(BigComplex c, bool posX, bool posY)
    {
        if (c.Real < 0 && posX)
            c.Real = 0;
        else if (c.Real > 0 && !posX)
            c.Real = 0;

        if (c.Imaginary < 0 && posY)
            c.Imaginary = 0;
        else if (c.Imaginary > 0 && !posY)
            c.Imaginary = 0;
        return c;
    }

    private static BigRational DistTo0(BigComplex c)
    {
        return SIPEP.Functions.Misc.Abs(c).Real;
    }
}