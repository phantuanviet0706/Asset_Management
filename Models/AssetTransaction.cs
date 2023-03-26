using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Transactions
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AssetId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string? TransactionType { get; set; }

    public double? TransactionCost { get; set; }

    public int? CreatedAt { get; set; }

    public virtual Assets? Asset { get; set; }
}
