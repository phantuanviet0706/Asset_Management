using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Users
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? UserCode { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? UserIdNumber { get; set; }

    public virtual ICollection<Assets> AssetAssignees { get; } = new List<Assets>();

    public virtual ICollection<Assets> AssetCreateByUserNavigations { get; } = new List<Assets>();
}
