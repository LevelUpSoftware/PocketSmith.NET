using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.CategoryRules;

public interface ICategoryRuleService
{
    Task<IList<PocketSmithCategoryRule>> GetAllAsync();

    Task<PocketSmithCategoryRule> CreateAsync(int categoryId, string payeeMatchString,
        bool applyToUnCategorized = false, bool applyToAll = false);
}