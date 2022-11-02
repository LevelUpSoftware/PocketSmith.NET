using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using System.Net.Http.Json;
using System.Text.Json;
using PocketSmith.NET.Constants;
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
        
        if (!validateCredentials(userId, apiKey))
        {
            throw new InvalidOperationException("The provided userId or apiKey is invalid.");
        }
        UserId = userId;
        UriBuilder = new UriBuilder(PocketSmithUriConstants.BASE_URI);
        ApiHelper = new ApiHelper(apiKey);
    }

    public virtual async Task<IEnumerable<TModel>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(TModel))
            .Uri;

        var results = await ApiHelper.GetAsync<List<TModel>>(uri);
        return results;
    }

    public virtual async Task<TModel> GetByIdAsync(TId id)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser)).AddRoute($"{id}").Uri;

        var httpResponse = await ApiHelper.HttpClient.GetAsync(uri);
        var responseContentString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsoluteUri, httpResponse.StatusCode, responseContentString);
        }

        var resultObject = JsonSerializer.Deserialize<TModel>(responseContentString);

        return resultObject;
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
            .Uri;

        var results = ApiHelper.HttpClient.GetAsync(uri).Result;
        return results.IsSuccessStatusCode;
    }
}