using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.TimeZones;

public interface ITimeZoneService
{
    Task<IEnumerable<PocketSmithTimeZone>> GetAllAsync();
}