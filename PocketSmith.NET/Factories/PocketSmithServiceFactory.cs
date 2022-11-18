using Microsoft.Extensions.DependencyInjection;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Services;

namespace PocketSmith.NET.Factories;

public static class PocketSmithServiceFactory
{
    private static readonly IServiceProvider _serviceProvider;

    static PocketSmithServiceFactory()
    {
        _serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
    }

    public static TService CreateService<TService>(int userId, string apiKey)
    where TService : IPocketSmithService, new()
    {
        IApiHelper apiHelper = ActivatorUtilities.CreateInstance<ApiHelper.ApiHelper>(_serviceProvider, apiKey);
        var service = ActivatorUtilities.CreateInstance<TService>(_serviceProvider, apiHelper, userId, apiKey);
        return service;
    }
}