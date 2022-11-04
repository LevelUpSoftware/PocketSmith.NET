using PocketSmith.NET.Exceptions;
using System.Net.Http.Json;
using System.Text.Json;

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
        var httpResponse = await HttpClient.PostAsJsonAsync(uri, requestBody);
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }

        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }

    public async Task<TApiModel> PutAsync<TApiModel>(Uri uri, object requestBody)
    {
        var httpResponse = await HttpClient.PutAsJsonAsync(uri, requestBody);
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }

        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }

    public async Task DeleteAsync(Uri uri)
    {
        var httpResponse = await HttpClient.DeleteAsync(uri);
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }
    }

    public async Task<TApiModel> GetAsync<TApiModel>(Uri uri)
    {
        var httpResponse = await HttpClient.GetAsync(uri);

        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }


        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject;
    }
}