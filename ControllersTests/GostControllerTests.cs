using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using ResourcesWebAPI.Controllers;

namespace ControllersTests
{
    public class GostControllerTests
    {
        [Fact]
        public void Get_ReturnsAllGosts()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            context.Gosts.AddRange(new Gost { GostId = 1, Name = "ГОСТ 1" }, new Gost { GostId = 2, Name = "ГОСТ 2" });
            context.SaveChanges();
            var controller = new GostController(context);

            // Act
            var result = controller.Get();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, g => g.Name == "ГОСТ 1");
        }

        [Fact]
        public void Get_ById_ReturnsGost_WhenExists()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            var gost = new Gost { GostId = 1, Name = "Test GOST" };
            context.Gosts.Add(gost);
            context.SaveChanges();
            var controller = new GostController(context);

            // Act
            var result = controller.Get(1);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var returnedGost = Assert.IsType<Gost>(objectResult.Value);
            Assert.Equal("Test GOST", returnedGost.Name);
        }

        [Fact]
        public void Get_ById_ReturnsNotFound_WhenNotExists()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);

            var result = controller.Get(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_ReturnsBadRequest_WhenNull()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);

            var result = controller.Post(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Post_CreatesGostAndReturnsOk()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);
            var newGost = new Gost { Name = "New GOST" };

            var result = controller.Post(newGost);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var created = Assert.IsType<Gost>(okResult.Value);
            Assert.Equal("New GOST", created.Name);
            Assert.NotEqual(0, created.GostId);

            // Проверяем, что сохранилось в БД
            var fromDb = context.Gosts.Find(created.GostId);
            Assert.NotNull(fromDb);
            Assert.Equal("New GOST", fromDb.Name);
        }

        [Fact]
        public void Put_ReturnsBadRequest_WhenNull()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);

            var result = controller.Put(null);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Put_ReturnsNotFound_WhenNotExists()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);
            var gost = new Gost { GostId = 999, Name = "Missing" };

            var result = controller.Put(gost);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_UpdatesGost_WhenExists()
        {
            var context = TestDbContextFactory.Create();
            var existing = new Gost { GostId = 1, Name = "Old" };
            context.Gosts.Add(existing);
            context.SaveChanges();

            context.Entry(existing).State = EntityState.Detached;

            var controller = new GostController(context);
            var updated = new Gost { GostId = 1, Name = "Updated" };

            var result = controller.Put(updated);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returned = Assert.IsType<Gost>(okResult.Value);
            Assert.Equal("Updated", returned.Name);

            var fromDb = context.Gosts.Find(1);
            Assert.NotNull(fromDb);
            Assert.Equal("Updated", fromDb.Name);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenNotExists()
        {
            var context = TestDbContextFactory.Create();
            var controller = new GostController(context);

            var result = controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_RemovesGost_WhenExists()
        {
            var context = TestDbContextFactory.Create();
            var gost = new Gost { GostId = 1, Name = "ToDelete" };
            context.Gosts.Add(gost);
            context.SaveChanges();

            var controller = new GostController(context);

            var result = controller.Delete(1);

            Assert.IsType<OkObjectResult>(result);
            Assert.Null(context.Gosts.Find(1));
        }
    }
}
