namespace SIPEP;

public enum OperatorStyle : byte
{
    None = 0,
    Left = 1,
    Right = 2,
    LeftAndRight = 4,
}

internal static class OperatorStyleExtensions
{
    public static bool IsLeft(this OperatorStyle operatorStyle)
    {
        return operatorStyle == OperatorStyle.Left || operatorStyle == OperatorStyle.LeftAndRight;
    }

    public static bool IsRight(this OperatorStyle operatorStyle)
    {
        return operatorStyle == OperatorStyle.Right || operatorStyle == OperatorStyle.LeftAndRight;
    }
}