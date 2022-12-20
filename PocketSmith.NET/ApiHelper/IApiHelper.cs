using PocketSmith.NET.Models;

namespace PocketSmith.NET.ApiHelper;

public interface IApiHelper
{
    public HttpClient HttpClient { get; }
    Task<TApiModel?> PostAsync<TApiModel>(Uri uri, object requestBody);

    Task<TApiModel?> PutAsync<TApiModel>(Uri uri, object requestBody);
    Task DeleteAsync(Uri uri);

    Task<TApiModel?> GetAsync<TApiModel>(Uri uri);
    Task<PocketSmithPagedQueryResult<TApiModel>> GetPagedAsync<TApiModel>(Uri uri);
    void SetApiKey(string apiKey);
}  