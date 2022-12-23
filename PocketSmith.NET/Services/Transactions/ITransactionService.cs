using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Transactions.Models;

namespace PocketSmith.NET.Services.Transactions;

public interface ITransactionService
{
    Task DeleteAsync(int id);
    Task<PocketSmithTransactionSummary> GetAllByAccountIdAsync(
        int accountId,
        int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransaction?> GetByIdAsync(int id);

    Task<PocketSmithTransactionSummary> GetAllAsync(int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransactionSummary> GetAllByCategoryIdAsync(int categoryId, int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransactionSummary> GetAllByTransactionAccountIdAsync(int transactionAccountId,
        int? pageNumber = null,
        PocketSmithTransactionSearch? searchParameters = null);

    Task<PocketSmithTransaction> CreateAsync(CreatePocketSmithTransaction createItem);

    Task<PocketSmithTransaction> UpdateAsync(UpdatePocketSmithTransaction updateItem, int id);

} 