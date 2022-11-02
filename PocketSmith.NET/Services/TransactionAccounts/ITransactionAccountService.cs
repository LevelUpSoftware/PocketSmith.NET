using PocketSmith.NET.Models;
using PocketSmith.NET.Services.TransactionAccounts.Models;

namespace PocketSmith.NET.Services.TransactionAccounts;

public interface ITransactionAccountService
{
    Task<PocketSmithTransactionAccount> GetByIdAsync(int id);
    Task<IEnumerable<PocketSmithTransactionAccount>> GetAllAsync();
    Task<PocketSmithTransactionAccount> UpdateAsync(UpdatePocketSmithTransactionAccount updateItem, int id);
}