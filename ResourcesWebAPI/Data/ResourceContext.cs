using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Resources.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.Resources.Core.Entities;

namespace Company.Resources.Infrastructure.Data
{
    public class ResourceContext : DbContext
    {
        public ResourceContext(DbContextOptions<ResourceContext> options)
            : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<ContractResource> ContractResources { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Gost> Gosts { get; set; }
        public DbSet<PlanSupply> PlanSupplys { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ResourceFlow> ResourcesFlows { get; set; }
        public DbSet<ResourceStock> ResourcesStocks { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }


        protected ResourceContext()
        {
        }
    }
}
