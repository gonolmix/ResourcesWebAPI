using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class PlanSupply
{
    public int PlanSupplyId { get; set; }

    public int ResourceId { get; set; }

    public int Year { get; set; }

    public byte Quarter { get; set; }

    public decimal PlannedQty { get; set; }

    public virtual Resource Resource { get; set; } = null!;
}
