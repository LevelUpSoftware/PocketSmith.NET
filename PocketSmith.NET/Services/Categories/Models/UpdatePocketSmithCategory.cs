﻿using PocketSmith.NET.Models;

namespace PocketSmith.NET.Services.Categories.Models;

public class UpdatePocketSmithCategory
{
    public string Title { get; set; }
    public string? Color { get; set; }
    public int? ParentId { get; set; }
    public bool? IsTransfer { get; set; }
    public bool? IsBill { get; set; }
    public bool? RollUp { get; set; }
    public PocketSmithRefundBehavior? RefundBehavior { get; set; }
}