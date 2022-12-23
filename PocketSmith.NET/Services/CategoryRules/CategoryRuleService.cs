using Microsoft.Extensions.Configuration;
using PocketSmith.NET.ApiHelper;
using PocketSmith.NET.Exceptions;
using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.CategoryRules;

public class CategoryRuleService : ServiceBase<PocketSmithCategoryRule, string>, ICategoryRuleService, IPocketSmithService
{
    public CategoryRuleService(IApiHelper apiHelper, IConfiguration configuration) : base(apiHelper, configuration)
    {
    }
    public CategoryRuleService(IApiHelper apiHelper, int userId, string apiKey) : base(apiHelper, userId, apiKey)
    {
    }
    public virtual async Task<PocketSmithCategoryRule> CreateAsync(int categoryId, string payeeMatchString, bool applyToUnCategorized = false, bool applyToAll = false)
    {
        if (string.IsNullOrEmpty(payeeMatchString))
        {
            throw new PocketSmithValidationException($"Argument {nameof(payeeMatchString)} is invalid.");
        }

        if (categoryId < 1)
        {
            throw new PocketSmithValidationException($"Argument {nameof(categoryId)} is invalid.");
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

    public new virtual async Task<IEnumerable<PocketSmithCategoryRule>> GetAllAsync()
    {
        var uri = UriBuilder
            .AddRouteFromModel(typeof(PocketSmithUser))
            .AddRoute(UserId.ToString())
            .AddRouteFromModel(typeof(PocketSmithCategoryRule))
            .GetUriAndReset();

        var response = await ApiHelper.GetAsync<List<PocketSmithCategoryRule>>(uri);
        return response ?? new List<PocketSmithCategoryRule>();
    }
}