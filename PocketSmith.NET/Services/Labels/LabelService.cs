using PocketSmith.NET.Extensions;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Labels;

public class LabelService : ServiceBase<string, int>, ILabelService
{
    public LabelService(int userId, string apiKey) : base(userId, apiKey)
    {
    }
}