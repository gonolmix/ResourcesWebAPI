using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class ResourceStock
{
    public int ResourceStockId { get; set; }

    public int ResourceId { get; set; }

    public int Year { get; set; }

    public decimal OpeningBalance { get; set; }

    public virtual Resource Resource { get; set; } = null!;
}
