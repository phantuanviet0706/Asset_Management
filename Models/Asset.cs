using System;
using System.Collections.Generic;

namespace Project_PRN.Models;

public partial class Asset
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public int? TypeId { get; set; }

    public int? LocationId { get; set; }

    public int? StatusId { get; set; }

    public int? AssigneeId { get; set; }

    public DateTime? AcquisitionDate { get; set; }

    public DateTime? DisposalDate { get; set; }

    public DateTime? AssignDate { get; set; }

    public int? VendorId { get; set; }

    public string? Description { get; set; }

    public int? TransactionId { get; set; }

    public int? CreateByUser { get; set; }

    public virtual User? Assignee { get; set; }

    public virtual User? CreateByUserNavigation { get; set; }

    public virtual AssetLocation? Location { get; set; }

    public virtual AssetStatus? Status { get; set; }

    public virtual AssetTransaction? Transaction { get; set; }

    public virtual AssetType? Type { get; set; }

    public virtual AssetVendor? Vendor { get; set; }
}
