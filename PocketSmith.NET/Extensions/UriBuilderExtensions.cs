using Microsoft.VisualBasic;
using PocketSmith.NET.Attributes;
using PocketSmith.NET.Constants;
using System.Reflection;

namespace PocketSmith.NET.Extensions;

public static class UriBuilderExtensions
{
	public static UriBuilder AddRoute(this UriBuilder builder, string route)
    {
        if (string.IsNullOrEmpty(route))
        {
            throw new ArgumentNullException(nameof(route), "Route cannot be null or empty.");
        }
        var characters = builder.Path.ToCharArray();
        if (characters.Last() != '/')
        {
            builder.Path += "/";
        }
        builder.Path += $"{route}";

        return builder;
    }

    public static UriBuilder AddQuery(this UriBuilder builder, string key, string value)
    {
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException(nameof(key), "Query parameter key cannot be null or empty.");
        }

        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value), "Query parameter value cannot be null or empty.");
        }

        var characters = builder.Path.ToCharArray();
        if (characters.Last() != '?' || !characters.Contains('?'))
        {
            builder.Path += "?";
        }

        if (characters.Contains('?'))
        {
            builder.Path += "&";
        }

        builder.Path += $"{key}={value}";

        return builder;
    }

    public static UriBuilder AddRouteFromModel(this UriBuilder builder, Type modelType)
    {
        HttpRouteAttribute? attribute = (HttpRouteAttribute)modelType.GetCustomAttribute(typeof(HttpRouteAttribute))!;
        if (string.IsNullOrEmpty(attribute.Route))
        {
            throw new InvalidOperationException($"Api route path cannot be retrieved for type {modelType.Name}. Verify the specified class has [HttpRoute()] attribute.");
        }

        return builder.AddRoute(attribute.Route);
    }
}