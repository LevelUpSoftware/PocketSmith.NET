using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions.Models;
using PocketSmith.NET.Services.Users.Models;

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
            .Uri;

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
            .Uri;
        await ApiHelper.DeleteAsync(uri);
    }

    public virtual async Task<IEnumerable<PocketSmithTransaction>> GetAllByAccountIdAsync(int accountId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .AddRoute(accountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .Uri;

        var results = await ApiHelper.GetAsync<List<PocketSmithTransaction>>(uri);

        return results;
    }

    public virtual async Task<IEnumerable<PocketSmithTransaction>> GetAllByCategoryAsync(int categoryId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uriBuilder = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .AddRoute(categoryId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction));

        if (searchParameters != null)
        {
            uriBuilder.AddQuery("start_date", searchParameters.StartDate.ToFormattedString())
                .AddQuery("end_date", searchParameters.EndDate.ToFormattedString())
                .AddQuery("updated_since", searchParameters.UpdatedSince.ToFormattedString())
                .AddQuery("uncategorized", searchParameters.Uncategorized.ToInteger().ToString())
                .AddQuery("type", searchParameters.Type.ToString().ToLower())
                .AddQuery("needs_review", searchParameters.NeedsReview.ToInteger().ToString())
                .AddQuery("search", searchParameters.Search);
        }

        if (pageNumber != null)
        {
            uriBuilder.AddQuery("page", pageNumber.Value.ToString());
        }

        var results = await ApiHelper.GetAsync<List<PocketSmithTransaction>>(uriBuilder.Uri);
        return results;
    }

    public virtual async Task<IEnumerable<PocketSmithTransaction>> GetAllByTransactionAccountIdAsync(int transactionAccountId, int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(transactionAccountId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .Uri;
        var results = await ApiHelper.GetAsync<List<PocketSmithTransaction>>(uri);
        return results;
    }

    public virtual async Task<IEnumerable<PocketSmithTransaction>> GetAllAsync(int? pageNumber = null, PocketSmithTransactionSearch? searchParameters = null)
    {
        var uri = UriBuilder.AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithTransaction)).Uri;

        var results = await ApiHelper.GetAsync<List<PocketSmithTransaction>>(uri);
        return results;
    }

    public virtual async Task<PocketSmithTransaction> UpdateAsync(UpdatePocketSmithTransaction updateItem, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransaction))
            .AddRoute(id.ToString())
            .Uri;

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
}