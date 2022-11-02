namespace PocketSmith.NET.Attributes;

public class HttpRouteAttribute : Attribute
{
    public string? Route { get; }
    public HttpRouteAttribute(string? route)
    {
        if (string.IsNullOrEmpty(route))
        {
            throw new ArgumentNullException(nameof(route), "Argument route cannot be null or empty.");
        }

        Route = route;
    }
}