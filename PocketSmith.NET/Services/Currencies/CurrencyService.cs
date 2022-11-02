using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Currencies;

public class CurrencyService : ServiceBase<PocketSmithCurrency, string>, ICurrencyService
{
    public CurrencyService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
}