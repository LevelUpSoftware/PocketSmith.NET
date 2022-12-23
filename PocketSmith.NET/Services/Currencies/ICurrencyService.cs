using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public interface ICurrencyService
{
    Task<IList<PocketSmithCurrency>> GetAllAsync();
    Task<PocketSmithCurrency?> GetByIdAsync(string id);
}