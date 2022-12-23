namespace PocketSmith.NET.Services.Labels;

public interface ILabelService
{
    Task<IEnumerable<string>> GetAllAsync();
}