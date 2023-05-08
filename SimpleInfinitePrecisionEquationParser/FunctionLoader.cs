using System.Reflection;

namespace SIPEP;

public static class FunctionLoader
{
    public static string Operators = "";
    public static int HighestOperatorOrder = 0;

    public static List<(FunctionAttribute, MethodInfo)>? loadedFunctions = null;
    //public static Dictionary<string, Func<BigComplex[], BigComplex>> customFunctions = new();
    public static Dictionary<string, (string equation, string varNameArgs)> customFunctions = new();
    private static Dictionary<char, int> getOperator = new();

    public static void AddFunction(string name, string variableNameArgs, string equation)
    {
        //customFunctions.Add(name, args => SolveCustomFunction(variableNameArgs, equation, args));
        if (customFunctions.ContainsKey(name))
            customFunctions[name] = (equation, variableNameArgs);
        else
            customFunctions.Add(name, (equation, variableNameArgs));
    }

    public static BigComplex DoFunction(string functionName, string args, Dictionary<string, BigComplex> variables)
    {
        loadedFunctions ??= LoadFunctions();
        int indexOfFunction = -1;

        for (int i = 0; i < loadedFunctions.Count; i++)
        {
            if (loadedFunctions[i].Item1.FunctionName.ToLower() != functionName.ToLower())
                continue;
            indexOfFunction = i;
            break;
        }

        var equations = SplitWithNonNestedEntries(args);
        if (indexOfFunction >= 0)
        {
            if (loadedFunctions[indexOfFunction].Item1.StringArguments)
            {
                var stringFunctionAnswer = loadedFunctions[indexOfFunction].Item2.Invoke(null, new object[] { variables, equations });
                if (stringFunctionAnswer is BigComplex stringFunctionAnswerBC)
                    return stringFunctionAnswerBC;
                throw new InvalidEquationException();
            }
        }

        BigComplex[] answers = new BigComplex[equations.Length];

        for (int i = 0; i < equations.Length; i++)
            answers[i] = new Equation(equations[i], variables).Solve();

        if (customFunctions.ContainsKey(functionName))
            return SolveCustomFunction(customFunctions[functionName].varNameArgs, customFunctions[functionName].equation, answers, variables);

        if (indexOfFunction < 0)
            throw new InvalidEquationException();

        if (!loadedFunctions[indexOfFunction].Item1.HandlesInfinity)
        {
            for (int i = 0; i < answers.Length; i++)
            {
                if (answers[i].IsInfinity)
                    return BigComplex.NaN;
            }
        }

        if (loadedFunctions[indexOfFunction].Item2.Invoke(null, new object[] { answers }) is BigComplex answer)
            return answer;

        //TODO: Invalid Equation Exception
        return BigComplex.Zero;
    }

    public static BigComplex SolveCustomFunction(string variableNameArgs, string equation, BigComplex[] args, Dictionary<string, BigComplex> variables)
    {
        Equation realEquation = new("", variables);
        string[] varNameArgs;
        if (string.IsNullOrEmpty(variableNameArgs))
            varNameArgs = Array.Empty<string>();
        else
            varNameArgs = variableNameArgs.Split(',');

        for (int i = 0; i < varNameArgs.Length; i++)
        {
            if (realEquation.Variables.ContainsKey(varNameArgs[i]))
                realEquation.Variables[varNameArgs[i]] = args[i];
            else
                realEquation.Variables.Add(varNameArgs[i], args[i]);
        }

        realEquation.LoadString(equation);
        return realEquation.Solve();
    }

    public static (FunctionAttribute oper, MethodInfo method) GetOperator(char op)
    {
        loadedFunctions ??= LoadFunctions();
        return loadedFunctions[getOperator[op]];
    }

    public static void ReloadFunctions()
    {
        loadedFunctions ??= LoadFunctions();
    }

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
