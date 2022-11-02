namespace PocketSmith.NET.Extensions;

public static class DateExtensions
{
    public static string ToFormattedString(this DateOnly date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}