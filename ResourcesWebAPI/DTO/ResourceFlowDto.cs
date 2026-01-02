using Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Resources.Core.DTO
{
    public class ResourceFlowDto
    {
        public int ResourceFlowId { get; set; }

        public int ResourceId { get; set; }

        public string ResourceName { get; set; } = string.Empty;

        public int Year { get; set; }

        public byte Quarter { get; set; }

        public decimal? DeliveredQty { get; set; }

        public decimal? UsedQty { get; set; }

    }
}
