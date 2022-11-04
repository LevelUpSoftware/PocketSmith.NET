using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions.Models;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PocketSmith.NET.Services.Transactions;

public class TransactionService : ServiceBase<PocketSmithTransaction, int>, ITransactionService 
{
    public TransactionService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<PocketSmithTransaction> CreateAsync(CreatePocketSmithTransaction createItem)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(createItem.TransactionAccountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .GetUriAndReset();

        var request = new
        {
            payee = createItem.Payee,
            amount = createItem.Amount,
            date = createItem.Date.ToFormattedString(),
            is_transfer = createItem.IsTransfer,
            labels = String.Join(",", createItem.Labels),
            category_id = createItem.CategoryId,
            note = createItem.Note,
            memo = createItem.Memo,
            cheque_number = createItem.CheckNumber.ToString(),
            needs_review = createItem.NeedsReview
        };

        return await ApiHelper.PostAsync<PocketSmithTransaction>(uri, request);
    }

    public virtual async Task DeleteAsync(int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(id.ToString())
            .GetUriAndReset();
        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllByAccountIdAsync(int accountId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .AddRoute(accountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddQuery("page", pageNumber.ToString());

        if (searchParameters != null)
        {
            uriBuilder.AddQuery("start_date", searchParameters.StartDate.ToFormattedString())
                .AddQuery("end_date", searchParameters.EndDate.ToFormattedString())
                .AddQuery("updated_since", searchParameters.UpdatedSince.ToString("o"))
                .AddQuery("uncategorised", searchParameters.Uncategorized.ToInteger().ToString())
                .AddQuery("type", searchParameters.Type?.ToString().ToLower())
                .AddQuery("needs_review", searchParameters.NeedsReview.ToInteger().ToString())
                .AddQuery("search", searchParameters.Search)
                .AddQuery("per_page", searchParameters.TransactionsPerPage.ToString());
        }

        var results = new PocketSmithTransactionSummary();

        var httpResponse = await ApiHelper.HttpClient.GetAsync(uriBuilder.GetUriAndReset());
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uriBuilder.Uri.AbsoluteUri, httpResponse.StatusCode, contentResponseString);
        }
        
        results.Transactions = JsonSerializer.Deserialize<List<PocketSmithTransaction>>(contentResponseString) ?? new List<PocketSmithTransaction>();

        results.TotalPages = getTotalPages(httpResponse.Headers);
        results.PageNumber = getCurrentPage(httpResponse.Headers);

        return results;
    }

    public virtual async Task<PocketSmithTransaction> GetByIdAsync(int id)
    {
        return await base.GetByIdAsync(id);
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllByCategoryAsync(int categoryId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .AddRoute(categoryId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddQuery("page", pageNumber.ToString());

        if (searchParameters != null)
        {
            uriBuilder.AddQuery("start_date", searchParameters.StartDate.ToFormattedString())
                .AddQuery("end_date", searchParameters.EndDate.ToFormattedString())
                .AddQuery("updated_since", searchParameters.UpdatedSince.ToString("o"))
                .AddQuery("uncategorised", searchParameters.Uncategorized.ToInteger().ToString())
                .AddQuery("type", searchParameters.Type?.ToString().ToLower())
                .AddQuery("needs_review", searchParameters.NeedsReview.ToInteger().ToString())
                .AddQuery("search", searchParameters.Search)
                .AddQuery("per_page", searchParameters.TransactionsPerPage.ToString());
        }

        var results = new PocketSmithTransactionSummary();

        var httpResponse = await ApiHelper.HttpClient.GetAsync(uriBuilder.GetUriAndReset());
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uriBuilder.Uri.AbsoluteUri, httpResponse.StatusCode, contentResponseString);
        }

        results.Transactions = JsonSerializer.Deserialize<List<PocketSmithTransaction>>(contentResponseString) ?? new List<PocketSmithTransaction>();

        results.TotalPages = getTotalPages(httpResponse.Headers);
        results.PageNumber = getCurrentPage(httpResponse.Headers);

        return results;
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllByTransactionAccountIdAsync(int transactionAccountId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(transactionAccountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddQuery("page", pageNumber.ToString());

        if (searchParameters != null)
        {
            uriBuilder.AddQuery("start_date", searchParameters.StartDate.ToFormattedString())
                .AddQuery("end_date", searchParameters.EndDate.ToFormattedString())
                .AddQuery("updated_since", searchParameters.UpdatedSince.ToString("o"))
                .AddQuery("uncategorised", searchParameters.Uncategorized.ToInteger().ToString())
                .AddQuery("type", searchParameters.Type?.ToString().ToLower())
                .AddQuery("needs_review", searchParameters.NeedsReview.ToInteger().ToString())
                .AddQuery("search", searchParameters.Search)
                .AddQuery("per_page", searchParameters.TransactionsPerPage.ToString());
        }

        var results = new PocketSmithTransactionSummary();

        var httpResponse = await ApiHelper.HttpClient.GetAsync(uriBuilder.GetUriAndReset());
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uriBuilder.Uri.AbsoluteUri, httpResponse.StatusCode, contentResponseString);
        }

        results.Transactions = JsonSerializer.Deserialize<List<PocketSmithTransaction>>(contentResponseString) ?? new List<PocketSmithTransaction>();

        results.TotalPages = getTotalPages(httpResponse.Headers);
        results.PageNumber = getCurrentPage(httpResponse.Headers);

        return results;
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllAsync(int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddQuery("page", pageNumber.ToString());

        if (searchParameters != null)
        {
            uriBuilder.AddQuery("start_date", searchParameters.StartDate.ToFormattedString())
                .AddQuery("end_date", searchParameters.EndDate.ToFormattedString())
                .AddQuery("updated_since", searchParameters.UpdatedSince.ToString("o"))
                .AddQuery("uncategorised", searchParameters.Uncategorized.ToInteger().ToString())
                .AddQuery("type", searchParameters.Type?.ToString().ToLower())
                .AddQuery("needs_review", searchParameters.NeedsReview.ToInteger().ToString())
                .AddQuery("search", searchParameters.Search)
                .AddQuery("per_page", searchParameters.TransactionsPerPage.ToString());
        }
        
        var results = new PocketSmithTransactionSummary();

        var httpResponse = await ApiHelper.HttpClient.GetAsync(uriBuilder.GetUriAndReset());
        var contentResponseString = await httpResponse.Content.ReadAsStringAsync();
        if (!httpResponse.IsSuccessStatusCode)
        {
            throw new RestApiException(uriBuilder.Uri.AbsoluteUri, httpResponse.StatusCode, contentResponseString);
        }

        results.Transactions = JsonSerializer.Deserialize<List<PocketSmithTransaction>>(contentResponseString) ?? new List<PocketSmithTransaction>();

        results.TotalPages = getTotalPages(httpResponse.Headers);
        results.PageNumber = getCurrentPage(httpResponse.Headers);

        return results;
    }

    public virtual async Task<PocketSmithTransaction> UpdateAsync(UpdatePocketSmithTransaction updateItem, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var request = new
        {
            memo = updateItem.Memo,
            cheque_number = updateItem.CheckNumber,
            payee = updateItem.Payee,
            amount = updateItem.Amount,
            date = updateItem.Date.ToFormattedString(),
            is_transfer = updateItem.IsTransfer,
            category_id = updateItem.CategoryId,
            note = updateItem.Note,
            needs_review = updateItem.NeedsReview,
            labels = string.Join(",", updateItem.Labels)
        };

        var response = await ApiHelper.PutAsync<PocketSmithTransaction>(uri, request);
        return response;
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
            results = int.Parse(Regex.Match(nextPageLink, "(?<=<)[^<]+(?=>;\\srel=\\\"next\\\")").Value) - 1;
        }
        return results;
    }
}