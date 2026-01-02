using Company.Resources.Core.Entities;
using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public string ContractNumber { get; set; } = null!;

    public DateOnly ContractDate { get; set; }

    public DateOnly Deadline { get; set; }

    public int SupplierId { get; set; }

    public int EmployeeId { get; set; }

    public virtual ICollection<ContractResource> ContractResources { get; set; } = new List<ContractResource>();

    public virtual Employee Employee { get; set; } = null!;
    public virtual Supplier Supplier { get; set; } = null!;
}
