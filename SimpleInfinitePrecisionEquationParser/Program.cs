using System;
using System.Numerics;

namespace SIPEP;

public class Program
{
    public static void Main(string[] args)
    {
        Equation eq = new("");
        while (true)
        {
            eq.LoadString(Console.ReadLine());
            Console.WriteLine(eq.Solve());
        }
        //expected 1 + nestedEquation
    }
}