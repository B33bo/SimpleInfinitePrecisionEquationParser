using SIPEP;
using System.Numerics;
using System.Text;

namespace CalcCommand;

public static class Program
{
    private static bool KeepOpen = true;
    public static void Main(string[] args)
    {
        FunctionLoader.ReloadFunctions();

        if (args.Length == 0)
        {
            ForeverMode();
            return;
        }

        CommandMode(args);
    }

    private static void CommandMode(string[] args)
    {
        Equation e = new("");
        string[] equations = args[0].Split(';');

        for (int i = 0; i < equations.Length; i++)
        {
            try
            {
                if (equations[i].StartsWith("solve"))
                {
                    Console.WriteLine(GetSolutionString(equations[i], e));
                    continue;
                }

                e.Parse(equations[i]);
                Console.WriteLine(e.Solve());
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }

    private static void ForeverMode()
    {
        while (KeepOpen)
        {
            string? input = Console.ReadLine();

            if (input is null)
                continue;

            try
            {
                if (input.StartsWith("solve"))
                {
                    Console.WriteLine(GetSolutionString(input, new Equation("")));
                    continue;
                }

                Console.WriteLine(new Equation(input).Solve());
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }

    private static string GetSolutionString(string s, Equation e)
    {
        // format: solve,varName,max,depth:
        var argsString = s.Split(':')[0];
        var args = argsString.Split(',');

        e.Parse(s[(argsString.Length + 1)..]);

        double max = 100;
        int depth = 20;
        BigRational cutoff = BigRational.Parse("0.1");
        bool realOnly = false;

        if (args.Length >= 3)
            max = double.Parse(args[2]);
        if (args.Length >= 4)
            depth = int.Parse(args[3]);
        if (args.Length >= 5)
            cutoff = BigRational.Parse(args[4]);
        if (args.Length >= 6)
            realOnly = bool.Parse(args[5]);

        var solutions = e.FindSolutions(args[1], max, depth, cutoff, realOnly);

        if (solutions.Length == 0)
            return "No Solutions";

        StringBuilder finalStr = new();
        for (int i = 0; i < solutions.Length; i++)
            finalStr.Append(solutions[i].ToString() + ", ");
        return finalStr.Remove(finalStr.Length - 2, 2).ToString();
    }

    [Function("Exit", StaticFunction = false)]
    public static BigComplex Exit(params BigComplex[] args)
    {
        KeepOpen = false;
        return 0;
    }

    [Function("TextColor", StaticFunction = false)]
    public static BigComplex TextColor(params BigComplex[] args)
    {
        Console.ForegroundColor = (ConsoleColor)(int)args[0].Real;
        return 0;
    }

    [Function("BGColor", StaticFunction = false)]
    public static BigComplex BGColor(params BigComplex[] args)
    {
        Console.BackgroundColor = (ConsoleColor)(int)args[0].Real;
        return 0;
    }
}