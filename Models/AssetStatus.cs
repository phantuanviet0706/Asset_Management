using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class AssetStatus
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AvailableToUse { get; set; }

    public int? Active { get; set; }

    public int? CurrentlyInUse { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
