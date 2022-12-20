using FluentValidation;
using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Accounts.Models;
using PocketSmith.NET.Services.Accounts.Validators;

namespace PocketSmith.NET.Services.Accounts;

public class AccountService : ServiceBase<PocketSmithAccount, int>, IAccountService, IPocketSmithService
{
    private readonly CreateAccountValidator _createValidator;

    public AccountService(IApiHelper apiHelper, IConfiguration configuration, CreateAccountValidator createValidator) : base(apiHelper, configuration)
    {
        _createValidator = createValidator;
    }
    public AccountService(IApiHelper apiHelper, int userId, string apiKey, CreateAccountValidator createValidator) : base(apiHelper, userId, apiKey)
    {
        _createValidator = createValidator;
    }

    public new virtual async Task<PocketSmithAccount> GetByIdAsync(int id)
    {
        return await base.GetByIdAsync(id);
    }

    public virtual async Task<PocketSmithAccount> UpdateAsync(UpdatePocketSmithAccount updateAccount, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .AddRoute(id.ToString())
            .GetUriAndReset();

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
            .GetUriAndReset();

        await ApiHelper.DeleteAsync(uri);
    }

    public new virtual async Task<IEnumerable<PocketSmithAccount>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public virtual async Task<IEnumerable<PocketSmithAccount>> UpdateDisplayOrder(List<int> accountIds)
    {
        var accounts = await GetAllAsync();
        if (!accounts.Any())
        {
            return new List<PocketSmithAccount>();
        }
        accounts = accounts.ToList();

        List<PocketSmithAccount> selectedAccounts = new List<PocketSmithAccount>();

        foreach (var id in accountIds)
        {
            var selectedAccount = accounts.FirstOrDefault(x => x.Id == id);

            if (selectedAccount == null)
            {
                continue;
            }
            selectedAccounts.Add(selectedAccount);
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .GetUriAndReset();

        var results = await ApiHelper.PutAsync<List<PocketSmithAccount>>(uri, selectedAccounts.ToArray());
        return results;
    }
    public virtual async Task<PocketSmithAccount> CreateAsync(CreatePocketSmithAccount createItem)
    {
        await _createValidator.ValidateAndThrowAsync(createItem);
        
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithAccount))
            .GetUriAndReset();

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
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithAccount>>(uri);
        return response;
    }
}