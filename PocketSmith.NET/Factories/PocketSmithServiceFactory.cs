using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Services;

namespace PocketSmith.NET.Factories;

public static class PocketSmithServiceFactory
{
    private static readonly IServiceCollection _serviceCollection;

    static PocketSmithServiceFactory()
    {
        _serviceCollection = new ServiceCollection().AddHttpClient();
    }

    public static TService CreateService<TService>(int userId, string apiKey)
    where TService : IPocketSmithService
    {
        var configurationSettings = new Dictionary<string, string>
        {
            { "pocketSmith:userId", userId.ToString() },
            { "pocketSmith:apiKey", apiKey }
        };
        var configurationSection = new Dictionary<string, Dictionary<string, string>>();

        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder.AddInMemoryCollection(configurationSettings);
        _serviceCollection.AddScoped<IConfiguration>(_ => configuration.Build());
        _serviceCollection.AddPocketSmith();

        var service = ActivatorUtilities.CreateInstance<TService>(_serviceCollection.BuildServiceProvider());
        return service;
    }
}