using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.CategoryRules;

public class CategoryRuleService : ServiceBase<PocketSmithCategoryRule, string>, ICategoryRuleService
{
    public CategoryRuleService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
    public virtual async Task<PocketSmithCategoryRule> CreateAsync(int categoryId, string payeeMatchString, bool applyToUnCategorized = false, bool applyToAll = false)
    {
        if (string.IsNullOrEmpty(payeeMatchString))
        {
            throw new ArgumentNullException(nameof(payeeMatchString));
        }

        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithCategory))
            .AddRoute(categoryId.ToString())
            .AddRouteFromModel(typeof(PocketSmithCategoryRule))
            .GetUriAndReset();

        var request = new
        {
            payee_matches = payeeMatchString,
            apply_to_uncategorised = applyToUnCategorized,
            apply_to_all = applyToAll
        };

        var response = await ApiHelper.PostAsync<PocketSmithCategoryRule>(uri, request);
        return response;
    }

    public virtual async Task<IEnumerable<PocketSmithCategoryRule>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithCategoryRule))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithCategoryRule>>(uri);
        return response;
    }
}