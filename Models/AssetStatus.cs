using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Statuses
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? AvailableToUse { get; set; }

    public int? Active { get; set; }

    public int? CurrentlyInUse { get; set; }

    public virtual ICollection<Assets> Assets { get; } = new List<Assets>();
}
