namespace PocketSmith.NET.Extensions;

public static class BooleanExtensions
{
    public static int? ToInteger(this bool? input)
    {
        if (input == null)
        {
            return null;
        }

        return input.Value ? 1 : 0;
    }
}