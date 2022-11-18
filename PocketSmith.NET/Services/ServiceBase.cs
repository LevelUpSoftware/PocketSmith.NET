using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Constants;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services;

public abstract class ServiceBase<TModel, TId>
where TModel: class
{
    protected IApiHelper ApiHelper { get; }
    protected UriBuilder UriBuilder { get; }
    protected int UserId { get; }

    protected ServiceBase(IApiHelper apiHelper, int userId, string apiKey)
    {
        UriBuilder = new UriBuilder(PocketSmithUriConstants.BASE_URI);
        ApiHelper = apiHelper;

        UserId = userId;
    }

    private protected async Task<IEnumerable<TModel>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(TModel))
            .GetUriAndReset();

        var results = await ApiHelper.GetAsync<List<TModel>>(uri);
        return results;
    }

    private protected async Task<TModel> GetByIdAsync(TId id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(TModel))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<TModel>(uri);
       
        return response;
    }
}