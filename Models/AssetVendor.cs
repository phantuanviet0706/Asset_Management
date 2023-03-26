using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Vendors
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ContactName { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Assets> Assets { get; } = new List<Assets>();
}
