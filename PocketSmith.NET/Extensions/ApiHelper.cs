using PocketSmith.NET.Constants;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Models;
using System.Net.Http.Json;
using System.Text.Json;
using PocketSmith.NET.JsonConverters;

namespace PocketSmith.NET.Extensions;

public class ApiHelper
{
    public static HttpClient HttpClient = new HttpClient();

    public ApiHelper(string apiKey)
    {
        HttpClient.DefaultRequestHeaders.Add("X-Developer-Key", apiKey);
    }

    public async Task<TApiModel> PostAsync<TApiModel>(Uri uri, object requestBody)
    {
        var httpResults = await HttpClient.PostAsJsonAsync(uri, requestBody);
        var contentResponseString = await httpResults.Content.ReadAsStringAsync();

        if (!httpResults.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResults.StatusCode, contentResponseString);
        }

        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }

    public async Task<TApiModel> PutAsync<TApiModel>(Uri uri, object requestBody)
    {
        var httpResults = await HttpClient.PutAsJsonAsync(uri, requestBody);
        var contentResponseString = await httpResults.Content.ReadAsStringAsync();

        if (!httpResults.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResults.StatusCode, contentResponseString);
        }

        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }

    public async Task DeleteAsync(Uri uri)
    {
        var httpResults = await HttpClient.DeleteAsync(uri);
        var contentResponseString = await httpResults.Content.ReadAsStringAsync();

        if (httpResults.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResults.StatusCode, contentResponseString);
        }
    }

    public async Task<TApiModel> GetAsync<TApiModel>(Uri uri)
    {
        var httpResults = await HttpClient.GetAsync(uri);

        var contentResponseString = await httpResults.Content.ReadAsStringAsync();

        if (!httpResults.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResults.StatusCode, contentResponseString);
        }


        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }
}