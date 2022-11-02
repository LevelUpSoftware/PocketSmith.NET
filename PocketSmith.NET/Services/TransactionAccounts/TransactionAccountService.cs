using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.TransactionAccounts.Models;

namespace PocketSmith.NET.Services.TransactionAccounts;

public class TransactionAccountService : ServiceBase<PocketSmithTransactionAccount, int>, ITransactionAccountService
{
    public TransactionAccountService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public async Task<PocketSmithTransactionAccount> UpdateAsync(UpdatePocketSmithTransactionAccount updateItem, int id)
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithTransactionAccount))
            .AddRoute(UserId.ToString())
            .Uri;

        var request = new
        {
            institution_id = updateItem.InstitutionId,
            starting_balance = updateItem.StartingBalance,
            starting_balance_date = updateItem.StartingBalanceDate.ToFormattedString()
        };

        var response = await ApiHelper.PutAsync<PocketSmithTransactionAccount>(uri, request);
        return response;
    }
}