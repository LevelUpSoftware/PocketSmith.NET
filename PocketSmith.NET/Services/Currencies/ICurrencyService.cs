using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public interface ICurrencyService
{
    Task<IEnumerable<PocketSmithCurrency>> GetAllAsync();
    Task<PocketSmithCurrency> GetByIdAsync(string id);
}