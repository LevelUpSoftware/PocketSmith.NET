using PocketSmith.NET.Constants;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services;

public abstract class ServiceBase<TModel, TId>
where TModel: class
{
    protected ApiHelper ApiHelper;
    protected readonly UriBuilder UriBuilder;
    protected int UserId { get; }

    protected ServiceBase(int userId, string apiKey)
    {
        UriBuilder = new UriBuilder(PocketSmithUriConstants.BASE_URI);
        ApiHelper = new ApiHelper(apiKey);


        if (!validateCredentials(userId, apiKey))
        {
            throw new InvalidOperationException("The provided userId or apiKey is invalid.");
        }
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
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<TModel>(uri);
       
        return response;
    }

    private bool validateCredentials(long userId, string apiKey)
    {
        if (userId < 1)
        {
            throw new ArgumentException($"{nameof(userId)} is invalid.");
        }

        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(userId.ToString())
            .GetUriAndReset();

        var results = ApiHelper.HttpClient.GetAsync(uri).Result;
        return results.IsSuccessStatusCode;
    }
}