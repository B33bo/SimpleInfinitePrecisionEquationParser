using System.Numerics;

namespace SIPEP;

public partial class Equation
{
    public static bool Radians { get; set; } = true;
    public const int Version = 13;
    public static int DecimalPrecision { get => BigRational.MaxDigits; set => BigRational.MaxDigits = value; }

    public Dictionary<string, Variable> Variables = Constants.Vars;
    public string[] positionalVars = Array.Empty<string>();

    private readonly List<(SectionType type, object data)> _data = new();

    public (SectionType type, object data)[] Data => _data.ToArray();

    public enum SectionType : byte
    {
        Number,         // 2, 424, 234i21, 12.3
        Variable,       // pi, x, my_silly_variable_name
        Function,       // sin, phase, rand
        Operators,      // +, -, *, /, ^
        NestedEquation, // (2 + 1)
        AssignVariable, // (x) = 3
        Ignore,
        Split,
    }

    public Equation(string equationStr, Dictionary<string, Variable> variables = null)
    {
        FunctionLoader.ReloadFunctions();
        if (variables is not null)
        {
            foreach (var item in variables)
            {
                if (Variables.ContainsKey(item.Key))
                    Variables[item.Key] = item.Value;
                else
                    Variables.Add(item.Key, item.Value);
            }
        }

        var newVars = Conversions.All;
        foreach (var var in newVars)
        {
            if (Variables.ContainsKey(var.Key))
                continue;
            Variables.Add(var.Key, var.Value);
        }

        Parse(equationStr);
    }

    public Equation(Equation other)
    {
        // should be direct copy
        _data = other._data;

        Variables = new();
        foreach (var item in other.Variables)
            Variables.Add(item.Key, item.Value);

        positionalVars = new string[other.positionalVars.Length];
        for (int i = 0; i < positionalVars.Length; i++)
            positionalVars[i] = other.positionalVars[i];
    }

    public void SetVariable(string name, BigComplex data)
    {
        if (Variables.ContainsKey(name))
        {
            if (Variables[name].IsConstant)
                return;
            Variables[name] = new Variable(data, false);
        }
        else
            Variables.Add(name, new Variable(data, false));
    }

    private BigComplex ToNumber(int index, List<(SectionType, object)> data)
    {
        if (data[index].Item1 == SectionType.Number)
        {
            if (data[index].Item2 is not BigComplex num)
                throw new InvalidEquationException();
            return num;
        }

        else if (data[index].Item1 == SectionType.Variable)
        {
            if (data[index].Item2 is not string varName)
                throw new InvalidEquationException();
            return GetVariable(varName);
        }

        throw new InvalidEquationException();
    }

    private BigComplex GetVariable(string name)
    {
        if (name.StartsWith("-"))
            return -Variables[name[1..]].Data;
        return Variables[name];
    }
}
