using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class AssetLocation
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Asset> Assets { get; } = new List<Asset>();
}
