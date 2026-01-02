using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using ResourcesWebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllersTests
{
    public class ResourceControllerTests
    {
        [Fact]
        public void Get_ReturnsAllResourcesWithGostName()
        {
            var context = TestDbContextFactory.Create();
            var gost = new Gost { GostId = 1, Name = "ГОСТ A" };
            var resource = new Resource { ResourceId = 1, Name = "Ресурс 1", GostId = 1, Characteristics = "Характ", Unit = "шт" };
            context.Gosts.Add(gost);
            context.Resources.Add(resource);
            context.SaveChanges();

            var controller = new ResourceController(context);
            var result = controller.Get();

            Assert.Single(result);
            Assert.Equal("Ресурс 1", result[0].Name);
            Assert.Equal("ГОСТ A", result[0].GostName);
        }

        [Fact]
        public void Get_ById_ReturnsResourceDto_WhenExists()
        {
            var context = TestDbContextFactory.Create();
            var gost = new Gost { GostId = 1, Name = "ГОСТ A" };
            var resource = new Resource { ResourceId = 1, Name = "Ресурс 1", GostId = 1, Characteristics = "Характ", Unit = "шт" };
            context.Gosts.Add(gost);
            context.Resources.Add(resource);
            context.SaveChanges();

            var controller = new ResourceController(context);
            var result = controller.Get(1);

            var objectResult = Assert.IsType<ObjectResult>(result);
            var dto = Assert.IsType<Company.Resources.Core.DTO.ResourceDto>(objectResult.Value);
            Assert.Equal("Ресурс 1", dto.Name);
            Assert.Equal("ГОСТ A", dto.GostName);
        }

        [Fact]
        public void Post_CreatesResource()
        {
            var context = TestDbContextFactory.Create();
            var controller = new ResourceController(context);
            var dto = new Company.Resources.Core.DTO.CreateResourceDto
            {
                Name = "Новый ресурс",
                GostId = null,
                Characteristics = "Характеристика",
                Unit = "кг"
            };

            var result = controller.Post(dto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var resource = Assert.IsType<Resource>(okResult.Value);
            Assert.Equal("Новый ресурс", resource.Name);
            Assert.NotNull(context.Resources.Find(resource.ResourceId));
        }

        [Fact]
        public void Put_UpdatesResource()
        {
            var context = TestDbContextFactory.Create();
            var existing = new Resource { ResourceId = 1, Name = "Старое", GostId = null };
            context.Resources.Add(existing);
            context.SaveChanges();

            context.Entry(existing).State = EntityState.Detached;

            var controller = new ResourceController(context);
            var dto = new Company.Resources.Core.DTO.UpdateResourceDto
            {
                Id = 1,
                Name = "Обновлённое",
                GostId = null,
                Characteristics = "Новые характеристики",
                Unit = "кг"
            };

            var result = controller.Put(dto);
            Assert.IsType<OkObjectResult>(result);

            var updated = context.Resources.Find(1);
            Assert.NotNull(updated);
            Assert.Equal("Обновлённое", updated.Name);
            Assert.Equal("Новые характеристики", updated.Characteristics);
            Assert.Equal("кг", updated.Unit);
        }

        [Fact]
        public void Delete_RemovesResource()
        {
            var context = TestDbContextFactory.Create();
            var resource = new Resource { ResourceId = 1, Name = "Удалить" };
            context.Resources.Add(resource);
            context.SaveChanges();

            var controller = new ResourceController(context);
            var result = controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
            Assert.Null(context.Resources.Find(1));
        }
    }
}
