namespace SIPEP;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public sealed class FunctionAttribute : Attribute
{
    public FunctionAttribute(string functionName)
    {
        FunctionName = functionName;
    }

    public string FunctionName { get; set; }
    public string InverseFunctionName { get; set; } = "Invalid";
    public char Operator { get; set; } = '\0';
    public string Args { get; set; } = "";
    public int Priority { get; set; } = 0;
    public bool CustomBuilt { get; set; } = false;
    public OperatorStyle OperatorStyle { get; set; } = OperatorStyle.LeftAndRight;
}
