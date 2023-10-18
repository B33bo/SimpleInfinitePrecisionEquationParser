namespace SIPEP;

partial class Equation
{
    private void EnterParenthesis(ref string current, ref int indentLevel)
    {
        indentLevel++;

        if (indentLevel != 1)
        {
            current += '(';
            return;
        }

        if (current == "")
            return;

        // if the bit before the parenthesis is a text like a variable, make it a function instead
        var type = GetTypeOfPart(current, out object dat);
        if (type == SectionType.Variable)
            type = SectionType.Function;

        _data.Add((type, dat));
        current = "";
    }

    private void ExitParenthesis(ref string current, ref int indentLevel)
    {
        indentLevel--;

        if (indentLevel > 0)
        {
            current += ")";
            return;
        }

        if (indentLevel != 0)
            return;

        // A thing in brackets could either be a nested equation: 1 + (2+2) OR parameters rand(0,5+5)
        SectionType paramsOrEquation = SectionType.NestedEquation;

        _data.Add((paramsOrEquation, current));
        current = "";
    }

    public void Parse(string equationStr)
    {
        _data.Clear();

        if (equationStr.StartsWith("let "))
        {
            InstructionalParse(equationStr);
            return;
        }

        string current = "";
        int indentLevel = 0;

        for (int i = 0; i < equationStr.Length; i++)
        {
            if (equationStr[i] == ' ' && indentLevel == 0) // spaces are important for some functions when not indented
                continue;

            if (equationStr[i] == '(')
            {
                EnterParenthesis(ref current, ref indentLevel);
                continue;
            }

            if (equationStr[i] == ')')
            {
                ExitParenthesis(ref current, ref indentLevel);
                continue;
            }

            // equation in an equation. Do not interfere.
            if (indentLevel != 0)
            {
                current += equationStr[i];
                continue;
            }

            if (equationStr[i] == ',')
            {
                if (current == "")
                {
                    _data.Add((SectionType.Split, null));
                    continue;
                }

                var type = GetTypeOfPart(current, out object dat);
                _data.Add((type, dat));
                current = "";
                _data.Add((SectionType.Split, null));
                continue;
            }

            if (FunctionLoader.Operators.Contains(equationStr[i]))
            {
                if (current != "")
                {
                    // isn't operator, something else was typed
                    var type = GetTypeOfPart(current, out object dat);
                    _data.Add((type, dat));
                }

                current = "";
                _data.Add((SectionType.Operators, equationStr[i]));
                continue;
            }

            current += equationStr[i];
        }

        if (current != "")
        {
            var finaltype = GetTypeOfPart(current, out object finaldat);
            _data.Add((finaltype, finaldat));
        }

        if (indentLevel != 0)
            throw new InvalidEquationException();
    }

    private void InstructionalParse(string str)
    {
        str = str[4..]; //removes the "let"

        int indexOfEquals = -1;
        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] != '=')
                continue;
            indexOfEquals = i;
            break;
        }

        if (indexOfEquals < 0)
            throw new InvalidEquationException();

        string varName = str[..(indexOfEquals)].Replace(" ", "");
        string equationStr = str[(indexOfEquals + 1)..];

        if (varName.EndsWith(")"))
        {
            // it's a function
            var functionName = varName.Split('(')[0];
            var args = varName.Split('(')[1][..^1];
            args = args.Trim();
            _data.Add((SectionType.AssignVariable, functionName));
            _data.Add((SectionType.NestedEquation, args));
            _data.Add((SectionType.NestedEquation, equationStr));

            return;
        }

        _data.Add((SectionType.AssignVariable, varName));
        _data.Add((SectionType.Number, new Equation(equationStr, Variables).Solve()));
    }

    private static SectionType GetTypeOfPart(string s, out object data)
    {
        data = s;
        if (s.StartsWith("(") && s.EndsWith(")"))
            return SectionType.NestedEquation;

        if (s == "-")
        {
            data = '-';
            return SectionType.Operators;
        }

        if (BigComplex.TryParse(s, out BigComplex numData))
        {
            data = numData;
            return SectionType.Number;
        }

        return SectionType.Variable; //functions get converted later
    }
}
