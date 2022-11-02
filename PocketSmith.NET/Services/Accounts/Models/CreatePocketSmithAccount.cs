using System.Diagnostics.Contracts;
using System.Text.Json.Serialization;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Accounts.Models;

public class CreatePocketSmithAccount
{
    public int UserId { get; set; }

    public int InstitutionId { get; set; }

    public string Title { get; set; }

    public string CurrencyCode { get; set; }

    public PocketSmithAccountType Type { get; set; }
}