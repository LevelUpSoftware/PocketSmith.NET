using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Institutions.Models;

namespace PocketSmith.NET.Services.Institutions;

public interface IInstitutionService
{
    Task<PocketSmithInstitution> GetByIdAsync(int id);
    Task<PocketSmithInstitution> UpdateAsync(UpdatePocketSmithInstitution updatedInstitution, int id);
    Task DeleteAsync(int id);
    Task<IEnumerable<PocketSmithInstitution>> GetAllAsync();
    Task<PocketSmithInstitution> CreateAsync(CreatePocketSmithInstitution createItem);
}