using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Labels;

public class LabelService : ServiceBase<string, int>, ILabelService, IPocketSmithService
{
    public LabelService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public LabelService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }

    public new virtual async Task<IEnumerable<string>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRoute("labels")
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<string>>(uri);
        return response ?? new List<string>();
    }
}