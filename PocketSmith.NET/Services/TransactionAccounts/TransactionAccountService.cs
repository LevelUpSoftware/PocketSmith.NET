using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Factories;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.TransactionAccounts.Models;

namespace PocketSmith.NET.Services.TransactionAccounts;

public class TransactionAccountService : ServiceBase<PocketSmithTransactionAccount, int>, ITransactionAccountService, IPocketSmithService
{
    public TransactionAccountService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper,
        configuration)
    {
    }
    public TransactionAccountService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }

    public new virtual async Task<PocketSmithTransactionAccount?> GetByIdAsync(int id)
    {
        return await base.GetByIdAsync(id);
    }

    public new virtual async Task<IList<PocketSmithTransactionAccount>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public async Task<PocketSmithTransactionAccount> UpdateAsync(UpdatePocketSmithTransactionAccount updateItem, int id)
    {
        if (id < 1)
        {
            throw new PocketSmithValidationException($"A valid {nameof(id)} argument is required.");
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(UserId.ToString())
            .GetUriAndReset();

        var request = AnonymousTypeFactory
            .Build()
            .AddPropertyIfNotNull("institution_id", updateItem.InstitutionId)
            .AddPropertyIfNotNull("starting_balance", updateItem.StartingBalance)
            .AddPropertyIfNotNull("starting_balance_date", updateItem.StartingBalanceDate)
            .Create();

        var response = await ApiHelper.PutAsync<PocketSmithTransactionAccount>(uri, request);
        return response;
    }
}