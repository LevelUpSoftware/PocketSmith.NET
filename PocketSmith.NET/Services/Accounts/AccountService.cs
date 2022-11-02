using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Accounts.Models;

namespace PocketSmith.NET.Services.Accounts;

public class AccountService : ServiceBase<PocketSmithAccount, int>, IAccountService
{
    public AccountService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<PocketSmithAccount> UpdateAsync(UpdatePocketSmithAccount updateAccount, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .AddRoute(id.ToString())
            .Uri;

        var requestObject = new
        {
            title = updateAccount.Title,
            currency_code = updateAccount.CurrencyCode,
            type = updateAccount.Type.ToString(),
            is_net_worth = updateAccount.IsNetWorth
        };

        var response = await ApiHelper.PutAsync<PocketSmithAccount>(uri, requestObject);
        return response;
    }

    public virtual async Task DeleteAsync(int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .AddRoute(id.ToString())
            .Uri;

        await ApiHelper.DeleteAsync(uri);
    }
    public virtual async Task<IEnumerable<PocketSmithAccount>> UpdateDisplayOrder(List<int> accountIds)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .Uri;

        var results = await ApiHelper.PutAsync<List<PocketSmithAccount>>(uri, accountIds.ToArray());
        return results;
    }
    public virtual async Task<PocketSmithAccount> CreateAsync(CreatePocketSmithAccount createItem)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(createItem.UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .Uri;

        var request = new
        {
            institution_id = createItem.InstitutionId,
            title = createItem.Title,
            currency_code = createItem.CurrencyCode,
            type = createItem.Type.ToString()
        };
        var response = await ApiHelper.PostAsync<PocketSmithAccount>(uri, request);
        return response;
    }
    public virtual async Task<IEnumerable<PocketSmithAccount>> GetAllByInstitutionIdAsync(int institutionId)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithInstitution))
            .AddRoute(institutionId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .Uri;

        var response = await ApiHelper.GetAsync<List<PocketSmithAccount>>(uri);
        return response;
    }
}