using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public class CurrencyService : ServiceBase<PocketSmithCurrency, string>, ICurrencyService, IPocketSmithService
{
    public CurrencyService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public CurrencyService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }

    public new virtual async Task<IEnumerable<PocketSmithCurrency>> GetAllAsync()
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithCurrency))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithCurrency>>(uri);
        return response ?? new List<PocketSmithCurrency>();
    }

    public new virtual async Task<PocketSmithCurrency?> GetByIdAsync(string id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCurrency))
            .AddRoute(id)
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<PocketSmithCurrency>(uri);
        return response;
    }
}