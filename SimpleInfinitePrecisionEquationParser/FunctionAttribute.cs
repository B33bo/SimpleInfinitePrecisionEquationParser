namespace SIPEP;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class FunctionAttribute : Attribute
{
    public FunctionAttribute(string functionName)
    {
        FunctionName = functionName;
    }

    public string FunctionName { get; set; }
    public string InverseFunctionName { get; set; } = "";
    public char Operator { get; set; } = '\0';
    public string Args { get; set; } = "";
    public int Priority { get; set; } = 0;
    public bool HandlesInfinity { get; set; } = false;
    public bool StringArguments { get; set; } = false;
    public bool StaticFunction { get; set; } = true;

    public OperatorStyle OperatorStyle { get; set; } = OperatorStyle.LeftAndRight;

    public override string ToString()
    {
        string functionExample = FunctionName + "()";
        if (Args != "")
            functionExample = Args;

        if (Operator != '\0')
            functionExample = Operator + " ( " + functionExample + " )";

        return functionExample;
    }
}
