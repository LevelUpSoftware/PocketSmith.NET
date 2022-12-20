using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace PocketSmith.NET.Factories;

internal class TempConfiguration : IConfiguration
{
    private Dictionary<string, IConfigurationSection> _config = new Dictionary<string, IConfigurationSection>();
    public IConfigurationSection GetSection(string key)
    {
        return _config.FirstOrDefault(x => x.Key == key).Value;
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        throw new NotImplementedException();
    }

    public IChangeToken GetReloadToken()
    {
        throw new NotImplementedException();
    }

    public string this[string key]
    {
        get => _config.FirstOrDefault(x => x.Key == key).Value.ToString();
        set => throw new NotImplementedException();
    }
}