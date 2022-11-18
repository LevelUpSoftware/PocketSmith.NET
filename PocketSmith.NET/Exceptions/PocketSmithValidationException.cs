namespace PocketSmith.NET.Exceptions;

public class PocketSmithValidationException : Exception
{
    public PocketSmithValidationException(string message) : base(message) { }
}