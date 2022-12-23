using PocketSmith.NET.Models;
using PocketSmith.NET.Services.Users.Models;

namespace PocketSmith.NET.Services.Users;

public interface IUserService
{
    Task<PocketSmithUser?> GetAuthorizedUserAsync();
    Task<PocketSmithUser?> GetAsync();
    Task<PocketSmithUser> UpdateAsync(UpdatePocketSmithUser updateItem);
}