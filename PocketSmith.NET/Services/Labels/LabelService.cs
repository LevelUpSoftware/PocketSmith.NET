using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Labels;

public class LabelService : ServiceBase<string, int>, ILabelService
{
    public LabelService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<IEnumerable<string>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRoute("labels")
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<string>>(uri);
        return response;
    }
}