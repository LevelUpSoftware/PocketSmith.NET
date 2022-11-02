namespace PocketSmith.NET.Extensions;

public static class BooleanExtensions
{
    public static int ToInteger(this bool input)
    {
        return input ? 1 : 0;
    }
}