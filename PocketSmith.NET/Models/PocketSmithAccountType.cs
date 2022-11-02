using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DisplayAttribute = System.ComponentModel.DataAnnotations.DisplayAttribute;

namespace PocketSmith.NET.Models;

public enum PocketSmithAccountType
{
    [Display(Name = "bank")]
    Bank,
    [Display(Name = "credits")]
    Credits,
    [Display(Name = "cash")]
    Cash,
    [Display(Name = "loans")]
    Loans,
    [Display(Name = "mortgage")]
    Mortgage,
    [Display(Name = "stocks")]
    Stocks,
    [Display(Name = "vehicle")]
    Vehicle,
    [Display(Name = "property")]
    Property,
    [Display(Name = "insurance")]
    Insurance,
    [Display(Name = "other_asset")]
    OtherAsset,
    [Display(Name = "other_liability")]
    OtherLiability
}