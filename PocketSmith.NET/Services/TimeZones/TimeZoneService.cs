using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.TimeZones;

public class TimeZoneService : ServiceBase<PocketSmithTimeZone, int>, ITimeZoneService
{
    public TimeZoneService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
}