using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Constants;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services;

public abstract class ServiceBase<TModel, TId>
{
    protected IApiHelper ApiHelper { get; }
    protected UriBuilder UriBuilder { get; }
    protected int UserId { get; }

    protected ServiceBase(IApiHelper apiHelper, int userId, string apiKey)
    {
        UriBuilder = new UriBuilder(PocketSmithUriConstants.BASE_URI);
        ApiHelper = apiHelper;
        ApiHelper.SetApiKey(apiKey);

        UserId = userId;
    }

    protected ServiceBase(IApiHelper apiHelper, IConfiguration configuration)
    {
        UriBuilder = new UriBuilder(PocketSmithUriConstants.BASE_URI);
        ApiHelper = apiHelper;

        var apiKey = configuration.GetSection("pocketSmith:apiKey").Value;
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new InvalidOperationException("Configuration does not contain a valid apiKey.");
        }
        ApiHelper.SetApiKey(apiKey);

        var userId = configuration.GetSection("pocketSmith:userId").Value;
        var parseSuccess = int.TryParse(userId, out int parsedUserId);

        if (!parseSuccess || parsedUserId == 0)
        {
            throw new InvalidOperationException("Configuration does not contain a valid userId.");
        }

        UserId = parsedUserId;
    }

    private protected async Task<IEnumerable<TModel>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(TModel))
            .GetUriAndReset();

        var results = await ApiHelper.GetAsync<List<TModel>>(uri);
        return results ?? new List<TModel>();
    }

    private protected async Task<TModel?> GetByIdAsync(TId id)
    {
        if (id == null || id.Equals(""))
        {
            throw new PocketSmithValidationException("Id cannot be null.");
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(TModel))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<TModel>(uri);
       
        return response;
    }
}