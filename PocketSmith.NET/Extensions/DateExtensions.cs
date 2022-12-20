namespace PocketSmith.NET.Extensions;

public static class DateExtensions
{
    public static string? ToFormattedString(this DateOnly? date)
    {
        if (date == null)
        {
            return null;
        }

        return date.Value.ToString("yyyy-MM-dd");
    }

    public static string? ToFormattedString(this DateOnly date)
    {
        return date.ToString("yyyy-MM-dd");
    }

    public static string? ToString(this DateTime? dateTime, string format)
    {
       return dateTime.HasValue ? dateTime.Value.ToString(format) : null;
    }
}