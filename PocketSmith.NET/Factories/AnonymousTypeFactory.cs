using System.Dynamic;

namespace PocketSmith.NET.Factories;

public class AnonymousTypeFactory
{
    private ExpandoObject _outputObject = new ExpandoObject();

    public static AnonymousTypeFactory Build()
    {
        return new AnonymousTypeFactory();
    }

    public AnonymousTypeFactory AddPropertyIfNotNull(string propertyName, object? value, bool valueToString = false)
    {
        if (value != null)
        {
            _outputObject.TryAdd(propertyName, valueToString ? value.ToString() : value);
        }
        return this;
    }

    public dynamic Create()
    {
        return _outputObject as dynamic;
    }
}