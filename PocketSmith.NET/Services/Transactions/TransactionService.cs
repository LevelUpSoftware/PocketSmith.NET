using FluentValidation;
using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Factories;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions.Models;
using PocketSmith.NET.Services.Transactions.Validators;

namespace PocketSmith.NET.Services.Transactions;

public class TransactionService : ServiceBase<PocketSmithTransaction, int>, ITransactionService, IPocketSmithService
{
    private readonly CreateTransactionValidator _createValidator;

    public TransactionService(IApiHelper apiHelper, IConfiguration configuration,
        CreateTransactionValidator createValidator) : base(apiHelper, configuration)
    {
        _createValidator = createValidator;
    }
    public TransactionService(IApiHelper apiHelper, int userId, string apiKey, CreateTransactionValidator createValidator) : base(apiHelper, userId, apiKey)
    {
        _createValidator = createValidator;
    }

    public virtual async Task<PocketSmithTransaction> CreateAsync(CreatePocketSmithTransaction createItem)
    {
        await _createValidator.ValidateAndThrowAsync(createItem);

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(createItem.TransactionAccountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .GetUriAndReset();

        var request = AnonymousTypeFactory.Build()
            .AddPropertyIfNotNull("payee", createItem.Payee)
            .AddPropertyIfNotNull("amount", createItem.Amount)
            .AddPropertyIfNotNull("date", createItem.Date.ToFormattedString())
            .AddPropertyIfNotNull("is_transfer", createItem.IsTransfer)
            .AddPropertyIfNotNull("labels", createItem.Labels.Any() ? string.Join(",", createItem.Labels) : null)
            .AddPropertyIfNotNull("category_id", createItem.CategoryId)
            .AddPropertyIfNotNull("note", createItem.Note)
            .AddPropertyIfNotNull("memo", createItem.Memo)
            .AddPropertyIfNotNull("cheque_number", createItem.CheckNumber)
            .AddPropertyIfNotNull("needs_review", createItem.NeedsReview)
            .Create();

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
        var apiResults = await ApiHelper.GetPagedAsync<PocketSmithTransaction>(uriBuilder.GetUriAndReset());

        results.TotalPages = apiResults.TotalPages;
        results.PageNumber = apiResults.CurrentPage;
        results.Transactions = apiResults.Results;
        return results;
    }

    public new virtual async Task<PocketSmithTransaction> GetByIdAsync(int id)
    {
        return await base.GetByIdAsync(id);
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllByCategoryIdAsync(int categoryId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
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
        var apiResults = await ApiHelper.GetPagedAsync<PocketSmithTransaction>(uriBuilder.GetUriAndReset());

        results.TotalPages = apiResults.TotalPages;
        results.PageNumber = apiResults.CurrentPage;
        results.Transactions = apiResults.Results;
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
        var apiResults = await ApiHelper.GetPagedAsync<PocketSmithTransaction>(uriBuilder.GetUriAndReset());

        results.TotalPages = apiResults.TotalPages;
        results.PageNumber = apiResults.CurrentPage;
        results.Transactions = apiResults.Results;

        return results;
    }

    public virtual async Task<PocketSmithTransactionSummary> GetAllAsync(int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
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
        var apiResults = await ApiHelper.GetPagedAsync<PocketSmithTransaction>(uriBuilder.GetUriAndReset());

        results.TotalPages = apiResults.TotalPages;
        results.PageNumber = apiResults.CurrentPage;
        results.Transactions = apiResults.Results;

        return results;
    }

    public virtual async Task<PocketSmithTransaction> UpdateAsync(UpdatePocketSmithTransaction updateItem, int id)
    {
        if (id < 1)
        {
            throw new PocketSmithValidationException($"Argument {nameof(id)} is invalid. Must be greater than 0.");
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(id.ToString())
            .GetUriAndReset();

        var request = AnonymousTypeFactory
            .Build()
            .AddPropertyIfNotNull("memo", updateItem.Memo)
            .AddPropertyIfNotNull("cheque_number", updateItem.CheckNumber, true)
            .AddPropertyIfNotNull("payee", updateItem.Payee)
            .AddPropertyIfNotNull("amount", updateItem.Amount)
            .AddPropertyIfNotNull("date", updateItem.Date.ToFormattedString())
            .AddPropertyIfNotNull("is_transfer", updateItem.IsTransfer)
            .AddPropertyIfNotNull("category_id", updateItem.CategoryId)
            .AddPropertyIfNotNull("note", updateItem.Note)
            .AddPropertyIfNotNull("needs_review", updateItem.NeedsReview)
            .AddPropertyIfNotNull("labels", updateItem.Labels.Any() ? string.Join(",", updateItem.Labels) : null)
            .Create();

        var response = await ApiHelper.PutAsync<PocketSmithTransaction>(uri, request);
        return response;
    }
}