using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class ResourceFlow
{
    public int ResourceFlowId { get; set; }

    public int ResourceId { get; set; }

    public int Year { get; set; }

    public byte Quarter { get; set; }

    public decimal? DeliveredQty { get; set; }

    public decimal? UsedQty { get; set; }

    public virtual Resource Resource { get; set; } = null!;
}
