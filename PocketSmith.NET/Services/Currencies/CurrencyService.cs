using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public class CurrencyService : ServiceBase<PocketSmithCurrency, string>, ICurrencyService
{
    public CurrencyService(int userId, string apiKey) : base(userId, apiKey)
    {
    }

    public virtual async Task<IEnumerable<PocketSmithCurrency>> GetAllAsync()
    {
        return await base.GetAllAsync();
    }

    public virtual async Task<PocketSmithCurrency> GetByIdAsync(string id)
    {
        return await base.GetByIdAsync(id);
    }
}