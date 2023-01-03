using System.Reflection;

namespace SIPEP;

public static class FunctionLoader
{
    public static string Operators = "";
    public static int HighestOperatorOrder = 0;

    private static List<(FunctionAttribute, MethodInfo)>? loadedFunctions = null;
    private static Dictionary<char, int> getOperator = new();

    public static BigComplex DoFunction(string functionName, string args, Dictionary<string, BigComplex> variables)
    {
        loadedFunctions ??= LoadFunctions();
        int indexOfFunction = 0;
        for (int i = 0; i < loadedFunctions.Count; i++)
        {
            if (loadedFunctions[i].Item1.FunctionName.ToLower() != functionName.ToLower())
                continue;
            indexOfFunction = i;
            break;
        }

        var equations = SplitWithNonNestedEntries(args);
        BigComplex[] answers = new BigComplex[equations.Length];

        for (int i = 0; i < equations.Length; i++)
            answers[i] = new Equation(equations[i], variables).Solve();

        if (loadedFunctions[indexOfFunction].Item2.Invoke(null, new object[] { answers }) is BigComplex answer)
            return answer;

        //TODO: Invalid Equation Exception
        return BigComplex.Zero;
    }

    public static (FunctionAttribute oper, MethodInfo method) GetOperator(char op)
    {
        loadedFunctions ??= LoadFunctions();
        return loadedFunctions[getOperator[op]];
    }

    public static void ReloadFunctions() => loadedFunctions = LoadFunctions();

    private static string[] SplitWithNonNestedEntries(string s)
    {
        List<string> val = new();
        string current = "";
        int nestCount = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                nestCount++;
            }

            if (s[i] == ')')
                nestCount--;

            if (nestCount >= 1)
            {
                current += s[i];
                continue;
            }

            if (s[i] == ',')
            {
                val.Add(current);
                current = "";
                continue;
            }

            current += s[i];
        }

        if (current != "")
            val.Add(current);
        return val.ToArray();
    }

    private static List<(FunctionAttribute, MethodInfo)> LoadFunctions()
    {
        getOperator = new Dictionary<char, int>();
        List<(FunctionAttribute, MethodInfo)> functions = new();

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            if (type.Namespace is null)
                continue;
            if (!type.Namespace.StartsWith("SIPEP.Functions"))
                continue;

            foreach (var method in type.GetMethods())
            {
                var attribute = method.GetCustomAttribute<FunctionAttribute>();
                if (attribute is null)
                    continue;
                if (attribute.Operator != '\0')
                {
                    getOperator.Add(attribute.Operator, functions.Count);
                    Operators += attribute.Operator;
                    if (attribute.Priority > HighestOperatorOrder)
                        HighestOperatorOrder = attribute.Priority;
                }

                functions.Add((attribute, method));
            }
        }

        return functions;
    }
}
