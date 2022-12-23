using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Users.Models;

namespace PocketSmith.NET.Services.Users;

public class UserService : ServiceBase<PocketSmithUser, int>, IUserService, IPocketSmithService
{
    public UserService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public UserService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey )
    {
    }

    public virtual async Task<PocketSmithUser?> GetAsync()
    {
        return await GetByIdAsync(UserId);
    }

    public virtual async Task<PocketSmithUser?> GetAuthorizedUserAsync()
    {
        var uri = UriBuilder.AddRoute("me").GetUriAndReset();
        var results = await ApiHelper.GetAsync<PocketSmithUser>(uri);
        return results;
    }

    public virtual async Task<PocketSmithUser> UpdateAsync(UpdatePocketSmithUser updateItem)
    {
        if (updateItem.WeekStartDay > 6)
        {
            throw new InvalidOperationException(
                "UpdatePocketSmithUser.WeekStartDay must be between 0 (Sunday) and 6 (Saturday).");
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .GetUriAndReset();

        var request = new
        {
            email = updateItem.Email,
            name = updateItem.Name,
            time_zone = updateItem.TimeZone,
            week_start_day = updateItem.WeekStartDay,
            beta_user = updateItem.BetaUser,
            base_currency_code = updateItem.BaseCurrencyCode,
            always_show_base_currency = updateItem.AlwaysShowBaseCurrency
        };

        var response = await ApiHelper.PutAsync<PocketSmithUser>(uri, request);
        return response;
    }
}