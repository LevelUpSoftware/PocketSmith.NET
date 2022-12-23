using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.SavedSearches;

public interface ISavedSearchService
{
    Task<IEnumerable<PocketSmithSavedSearch>> GetAllAsync();
}