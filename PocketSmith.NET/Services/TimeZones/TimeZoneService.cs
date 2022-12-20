using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.TimeZones;

public class TimeZoneService : ServiceBase<PocketSmithTimeZone, int>, ITimeZoneService, IPocketSmithService
{
    public TimeZoneService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public TimeZoneService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }

    public new virtual async Task<IEnumerable<PocketSmithTimeZone>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTimeZone))
            .GetUriAndReset();

        var results = await ApiHelper.GetAsync<List<PocketSmithTimeZone>>(uri);
        return results;
    }
}