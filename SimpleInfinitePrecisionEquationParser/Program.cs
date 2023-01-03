using System;
using System.Numerics;

namespace SIPEP;

public class Program
{
    public static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine(new Equation(Console.ReadLine()).Solve());
        }
        //expected 1 + nestedEquation
    }
}