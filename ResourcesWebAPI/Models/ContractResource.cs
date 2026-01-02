using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class ContractResource
{
    public int ContractResourceId { get; set; }

    public int ContractId { get; set; }

    public int ResourceId { get; set; }

    public decimal Qty { get; set; }

    public virtual Contract Contract { get; set; } = null!;

    public virtual Resource Resource { get; set; } = null!;
}
