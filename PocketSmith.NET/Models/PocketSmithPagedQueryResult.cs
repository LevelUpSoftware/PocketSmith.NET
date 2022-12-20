namespace PocketSmith.NET.Models;

public class PocketSmithPagedQueryResult<TApiModel>
{
    public PocketSmithPagedQueryResult()
    {
        Results = new List<TApiModel>();
    }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public List<TApiModel> Results { get; set; }
}