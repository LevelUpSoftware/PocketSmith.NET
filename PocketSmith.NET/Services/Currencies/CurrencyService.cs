using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public class CurrencyService : ServiceBase<PocketSmithCurrency, string>, ICurrencyService, IPocketSmithService
{
    public CurrencyService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
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