using System.Net;
using PocketSmith.NET.Exceptions;
using System.Text.Json;
using PocketSmith.NET.Models;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace PocketSmith.NET.ApiHelper;

public class ApiHelper : IApiHelper
{
    public HttpClient HttpClient { get; }
    public bool ApiKeySet { get; private set; }

    public ApiHelper(HttpClient httpClient)
    {
        HttpClient = httpClient;
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
        return resultObject!;
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
        return resultObject!;
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

    public async Task<TApiModel?> GetAsync<TApiModel>(Uri uri)
    {
        var httpResponse = await HttpClient.GetAsync(uri);

        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }


        var resultObject = JsonSerializer.Deserialize<TApiModel>(contentResponseString);
        return resultObject!;
    }

    public async Task<PocketSmithPagedQueryResult<TApiModel>> GetPagedAsync<TApiModel>(Uri uri)
    {
        var results = new PocketSmithPagedQueryResult<TApiModel>();

        var httpResponse = await HttpClient.GetAsync(uri);

        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();

        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uri.AbsolutePath, httpResponse.StatusCode, contentResponseString);
        }


        results.CurrentPage = getCurrentPage(httpResponse.Headers);
        results.TotalPages = getTotalPages(httpResponse.Headers);
        results.Results = JsonSerializer.Deserialize<List<TApiModel>>(contentResponseString) ?? new List<TApiModel>();

        return results;
    }

    public void SetApiKey(string apiKey)
    {
        if (string.IsNullOrEmpty(apiKey))
        {
            throw new ArgumentNullException(nameof(apiKey));
        }

        ApiKeySet = true;
        HttpClient.DefaultRequestHeaders.Add("X-Developer-Key", apiKey);
    }

    private int getTotalPages(HttpResponseHeaders headers)
    {
        var totalRecords = int.Parse(headers.GetValues("total").First());
        var perPage = int.Parse(headers.GetValues("per-page").First());
        var results = totalRecords / perPage;
        return results == 0 ? 1 : results;

    }

    private int getCurrentPage(HttpResponseHeaders headers)
    {
        var pageLinks = headers.GetValues("link").First();
        var nextPageLink = Regex.Match(pageLinks, "(?<=<)[^<]+(?=>;\\srel=\\\"next\\\")").Value;

        int results;
        if (string.IsNullOrEmpty(nextPageLink))
        {
            results = 1;
        }
        else
        {
            var nextPageString = Regex.Match(nextPageLink, @"(?<=page=)\d+").Value;
            results = int.Parse(nextPageString) - 1;
        }
        return results;
    }
}