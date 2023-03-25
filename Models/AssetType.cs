using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Types
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<AssetModel> Assets { get; } = new List<AssetModel>();
}
