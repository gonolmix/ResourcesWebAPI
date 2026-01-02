using Company.Resources.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ControllersTests
{
    public static class TestDbContextFactory
    {
        public static ResourceContext Create()
        {
            var options = new DbContextOptionsBuilder<ResourceContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            var context = new ResourceContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(ResourceContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
