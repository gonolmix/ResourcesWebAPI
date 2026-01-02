using Microsoft.AspNetCore.Mvc;
using Resources.Models;
using ResourcesWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersTests
{
    public class ResourcesFlowControllerTests
    {
        [Fact]
        public void Get_ReturnsAllFlowsWithResourceName()
        {
            var context = TestDbContextFactory.Create();
            var resource = new Resource { ResourceId = 1, Name = "Электроэнергия" };
            var flow = new ResourceFlow
            {
                ResourceFlowId = 1,
                ResourceId = 1,
                Year = 2025,
                Quarter = 1,
                DeliveredQty = 100,
                UsedQty = 90
            };
            context.Resources.Add(resource);
            context.ResourcesFlows.Add(flow);
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var result = controller.Get();

            Assert.Single(result);
            Assert.Equal("Электроэнергия", result[0].ResourceName);
            Assert.Equal(100, result[0].DeliveredQty);
        }

        [Fact]
        public void GetResourceFlowsByResourceName_FiltersCorrectly()
        {
            var context = TestDbContextFactory.Create();
            var r1 = new Resource { ResourceId = 1, Name = "Электроэнергия" };
            var r2 = new Resource { ResourceId = 2, Name = "Вода" };
            context.Resources.AddRange(r1, r2);
            context.ResourcesFlows.AddRange(
                new ResourceFlow { ResourceFlowId = 1, ResourceId = 1, Year = 2025, Quarter = 1 },
                new ResourceFlow { ResourceFlowId = 2, ResourceId = 2, Year = 2025, Quarter = 1 }
            );
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var result = controller.GetResourceFlowsByResourceName("электро");

            Assert.Single(result);
            Assert.Equal("Электроэнергия", result[0].ResourceName);
        }

        [Fact]
        public void GetResources_ReturnsAllResources()
        {
            var context = TestDbContextFactory.Create();
            context.Resources.Add(new Resource { ResourceId = 1, Name = "Тест" });
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var result = controller.GetResources();

            Assert.Single(result);
        }

        [Fact]
        public void Post_CreatesFlow()
        {
            var context = TestDbContextFactory.Create();
            context.Resources.Add(new Resource { ResourceId = 1, Name = "Ресурс" });
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var dto = new Company.Resources.Core.DTO.CreateResourceFlowDto
            {
                ResourceId = 1,
                Year = 2025,
                Quarter = 2,
                DeliveredQty = 50
            };

            var result = controller.Post(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var flow = Assert.IsType<ResourceFlow>(okResult.Value);
            Assert.Equal(50, flow.DeliveredQty);
        }

        [Fact]
        public void Put_UpdatesFlow()
        {
            var context = TestDbContextFactory.Create();
            context.Resources.Add(new Resource { ResourceId = 1, Name = "R" });
            var flow = new ResourceFlow
            {
                ResourceFlowId = 10,
                ResourceId = 1,
                Year = 2024,
                Quarter = 1,
                DeliveredQty = 100
            };
            context.ResourcesFlows.Add(flow);
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var dto = new Company.Resources.Core.DTO.UpdateResourceFlowDto
            {
                Id = 10,
                ResourceId = 1,
                Year = 2025,
                Quarter = 2,
                DeliveredQty = 200
            };

            var result = controller.Put(10, dto);

            Assert.IsType<OkObjectResult>(result);
            var updated = context.ResourcesFlows.Find(10);
            Assert.Equal(200, updated.DeliveredQty);
            Assert.Equal(2, updated.Quarter);
        }

        [Fact]
        public void Put_ReturnsBadRequest_WhenIdMismatch()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ResourcesFlowController(context);
            var dto = new Company.Resources.Core.DTO.UpdateResourceFlowDto { Id = 1 };

            var result = controller.Put(2, dto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Delete_RemovesFlow()
        {
            var context = TestDbContextFactory.Create();
            var flow = new ResourceFlow { ResourceFlowId = 1, ResourceId = 1, Year = 2025, Quarter = 1 };
            context.ResourcesFlows.Add(flow);
            context.SaveChanges();

            var controller = new ResourcesFlowController(context);
            var result = controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
            Assert.Null(context.ResourcesFlows.Find(1));
        }
    }
}
