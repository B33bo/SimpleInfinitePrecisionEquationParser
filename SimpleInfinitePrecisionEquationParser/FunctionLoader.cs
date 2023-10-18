using System.Linq;
using System.Reflection;

namespace SIPEP;

public static class FunctionLoader
{
    public static string Operators { get; private set; } = "";
    public static int HighestOperatorOrder { get; private set; } = 0;

    public static List<Function> loadedFunctions = null;
    public static Dictionary<string, CustomFunction> customFunctions = new()
    {
        {"quadraticpos", new CustomFunction(@"(-b+\(b^2-4*a*c))/(2*a)", "a,b,c") },
        {"quadraticneg", new CustomFunction(@"(-b-\(b^2-4*a*c))/(2*a)", "a,b,c") },
    };
    private static Dictionary<char, int> getOperator = new();

    public static void AddFunction(string name, string variableNameArgs, string equation)
    {
        if (customFunctions.ContainsKey(name))
            customFunctions[name] = new CustomFunction(equation, variableNameArgs);
        else
            customFunctions.Add(name, new CustomFunction(equation, variableNameArgs));
    }

    public static bool IsStringFunction(string name)
    {
        int index = IndexOfFunction(name);

        if (index < 0)
            return false;
        return loadedFunctions[index].FunctionInfo.StringArguments;
    }

    private static int IndexOfFunction(string functionName)
    {
        functionName = functionName.ToLower();
        for (int i = 0; i < loadedFunctions.Count; i++)
        {
            if (functionName != loadedFunctions[i].FunctionInfo.FunctionName.ToLower())
                continue;
            return i;
        }
        return -1;
    }

    public static BigComplex DoStringFunction(string functionName, Dictionary<string, Variable> variables, string argsS)
    {
        int index = IndexOfFunction(functionName);

        if (index < 0)
            throw new InvalidEquationException();

        var args = SplitWithNonNestedEntries(argsS);

        var stringFunctionAnswer = loadedFunctions[index].Method.Invoke(null, new object[] { variables, args });

        if (stringFunctionAnswer is not BigComplex stringFunctionAnswerBC)
            throw new InvalidEquationException();

        return stringFunctionAnswerBC;
    }

    public static BigComplex DoFunction(string functionName, string equation, Dictionary<string, Variable> variables)
    {
        loadedFunctions ??= LoadFunctions();
        int indexOfFunction = IndexOfFunction(functionName);

        BigComplex[] args = new Equation(equation, variables).SolveValues();

        if (customFunctions.ContainsKey(functionName))
            return SolveCustomFunction(customFunctions[functionName], args, variables);

        if (indexOfFunction < 0)
            throw new InvalidEquationException();

        if (!loadedFunctions[indexOfFunction].FunctionInfo.HandlesInfinity)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].IsInfinity)
                    return BigComplex.NaN;
            }
        }

        if (loadedFunctions[indexOfFunction].Method.Invoke(null, new object[] { args }) is BigComplex answer)
            return answer;

        throw new InvalidEquationException();
    }

    public static bool IsFunctionStatic(string functionName)
    {
        if (customFunctions.ContainsKey(functionName))
            return false;

        int index = IndexOfFunction(functionName);

        if (index < 0)
            return false;

        return loadedFunctions[index].FunctionInfo.StaticFunction;
    }

    public static BigComplex SolveCustomFunction(CustomFunction customFunction, BigComplex[] args, Dictionary<string, Variable> variables)
    {
        Equation realEquation = new("", variables);
        string[] varNameArgs;
        if (string.IsNullOrEmpty(customFunction.VarNameArgs))
            varNameArgs = Array.Empty<string>();
        else
            varNameArgs = customFunction.VarNameArgs.Split(',');

        for (int i = 0; i < varNameArgs.Length; i++)
        {
            if (realEquation.Variables.ContainsKey(varNameArgs[i]))
                realEquation.Variables[varNameArgs[i]] = args[i];
            else
                realEquation.Variables.Add(varNameArgs[i], args[i]);
        }

        realEquation.Parse(customFunction.Equation);
        return realEquation.Solve();
    }

    public static Function GetOperator(char op)
    {
        loadedFunctions ??= LoadFunctions();
        return loadedFunctions[getOperator[op]];
    }

    public static void ReloadFunctions()
    {
        loadedFunctions ??= LoadFunctions();
    }

    private static string[] SplitWithNonNestedEntries(string str)
    {
        List<string> val = new();
        string current = "";
        int nestCount = 0;

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '(')
                nestCount++;

            if (str[i] == ')')
                nestCount--;

            if (nestCount >= 1)
            {
                current += str[i];
                continue;
            }

            if (str[i] == ',')
            {
                val.Add(current);
                current = "";
                continue;
            }

            current += str[i];
        }

        if (current != "")
            val.Add(current);
        return val.ToArray();
    }

    private static List<Function> LoadFunctions()
    {
        getOperator = new Dictionary<char, int>();
        List<Function> functions = new();

        foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
        {
            foreach (var method in type.GetMethods())
            {
                var attribute = method.GetCustomAttribute<FunctionAttribute>();
                if (attribute is null)
                    continue;
                functions.Add(new Function(attribute, method));

                if (attribute.Operator == '\0')
                    continue;

                getOperator.Add(attribute.Operator, functions.Count - 1);
                Operators += attribute.Operator;
                if (attribute.Priority > HighestOperatorOrder)
                    HighestOperatorOrder = attribute.Priority;
            }
        }

        return functions;
    }
}
public record class Function(FunctionAttribute FunctionInfo, MethodInfo Method);
public record class CustomFunction(string Equation, string VarNameArgs);