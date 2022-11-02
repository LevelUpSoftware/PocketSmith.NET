using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Categories.Models;
using PocketSmith.NET.Services.Categories.Options;

namespace PocketSmith.NET.Services.Categories;

public interface ICategoryService
{
    Task<PocketSmithCategory> GetByIdAsync(int id);
    Task<IEnumerable<PocketSmithCategory>> GetAllAsync();
    Task<PocketSmithCategory> UpdateAsync(UpdatePocketSmithCategory updateItem, int id);
    Task DeleteAsync(int id);
    Task<PocketSmithCategory> CreateAsync(CreatePocketSmithCategory createItem);
}