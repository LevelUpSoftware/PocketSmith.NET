using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions.Models;
using PocketSmith.NET.Services.Users.Models;

namespace PocketSmith.NET.Services.Transactions;

public interface ITransactionService
{
    Task DeleteAsync(int id);
    Task<IEnumerable<PocketSmithTransaction>> GetAllByAccountIdAsync(
        int accountId,
        int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransaction> GetByIdAsync(int id);

    Task<IEnumerable<PocketSmithTransaction>> GetAllAsync(int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<IEnumerable<PocketSmithTransaction>> GetAllByCategoryAsync(int categoryId, int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<IEnumerable<PocketSmithTransaction>> GetAllByTransactionAccountIdAsync(int transactionAccountId,
        int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransaction> CreateAsync(CreatePocketSmithTransaction createItem);

    Task<PocketSmithTransaction> UpdateAsync(UpdatePocketSmithTransaction updateItem, int id);

} 