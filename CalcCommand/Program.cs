using SIPEP;

namespace CalcCommand;

public static class Program
{
    public static bool KeepOpen = true;
    public static void Main(string[] args)
    {
        FunctionLoader.ReloadFunctions();
#nullable disable
        FunctionLoader.loadedFunctions?.Add((new FunctionAttribute("Exit"), typeof(Program).GetMethod("Exit")));
        FunctionLoader.loadedFunctions?.Add((new FunctionAttribute("TextColor"), typeof(Program).GetMethod("TextColor")));
        FunctionLoader.loadedFunctions?.Add((new FunctionAttribute("BGColor"), typeof(Program).GetMethod("BGColor")));
#nullable enable

        if (args.Length == 0)
        {
            while (KeepOpen)
            {
                string? input = Console.ReadLine();

                if (input is null)
                    continue;

                try
                {
                    Console.WriteLine(new Equation(input).Solve());
                }
                catch (Exception)
                {
                    Console.WriteLine("Error");
                }
            }
            return;
        }

        Equation e = new("");
        string[] equations = args[0].Split(';');

        for (int i = 0; i < equations.Length; i++)
        {
            try
            {
                e.LoadString(equations[i]);
                if (i == equations.Length - 1)
                    Console.WriteLine(e.Solve());
            }
            catch (Exception)
            {
                Console.WriteLine("Error");
            }
        }
    }

    public static BigComplex Exit(params BigComplex[] args)
    {
        KeepOpen = false;
        return 0;
    }

    public static BigComplex TextColor(params BigComplex[] args)
    {
        Console.ForegroundColor = (ConsoleColor)(int)args[0].Real;
        return 0;
    }

    public static BigComplex BGColor(params BigComplex[] args)
    {
        Console.BackgroundColor = (ConsoleColor)(int)args[0].Real;
        return 0;
    }
}