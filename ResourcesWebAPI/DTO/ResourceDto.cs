using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Models;

namespace Company.Resources.Core.DTO
{
    public class ResourceDto
    {
        public int ResourceId { get; set; }

        public string Name { get; set; } = null!;

        public int? GostId { get; set; }

        public string? GostName { get; set; }

        public string? Characteristics { get; set; }

        public string? Unit { get; set; }

        public List<ResourceFlowDto> ResourceFlows { get; set; } = new();

    }
}
