using System.Net;

namespace PocketSmith.NET.Exceptions;

public class RestApiException : Exception
{
    public RestApiException(string uri, HttpStatusCode statusCode, string responseMessage) : base($"Call to endpoint {uri} failed with status code {statusCode} and the following error: {responseMessage}")
    {
    }
}