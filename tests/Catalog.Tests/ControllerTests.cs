using System.Security.Claims;
using Catalog.Controllers;
using Catalog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Catalog.Model;
using Moq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Catalog.Services;

namespace Catalog.Tests
{
    public class ControllerTests
    {
        [Fact]
        public void TestController_Index_Success()
        {
            //Arrange
            var controller = new TestController();
            //Act
            var result = controller.Index();
            //Assert
            Assert.IsType<ViewResult>(result);
        }


        [Fact]
        public void ProductController_Index_Sucess()
        {
            //Arrange
            // Arrange

            // Dbcontext Options
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Contexto
            var ctx = new AppDbContext(options);

            ctx.Products.Add(new Product { Id = 1, Name = "Produto 1", Value = "10" });
            ctx.Products.Add(new Product { Id = 2, Name = "Produto 2", Value = "10" });
            ctx.Products.Add(new Product { Id = 3, Name = "Produto 3", Value = "10" });
            ctx.SaveChanges();

            // Identity
            var mockClaimsIdentity = new Mock<ClaimsIdentity>();
            mockClaimsIdentity.Setup(m => m.Name).Returns("teste@teste.com");

            var principal = new ClaimsPrincipal(mockClaimsIdentity.Object);

            var mockContext = new Mock<HttpContext>();
            mockContext.Setup(c => c.User).Returns(principal);
            var imgService = new Mock<IImageUploadService>();
            // Controller
            var controller = new ProductsController(ctx, imgService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = mockContext.Object
                }
            };
            // Act
            var result = controller.Index().Result;

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ProductController_Create_Sucess()
        {
            // Dbcontext Options
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Contexto
            var ctx = new AppDbContext(options);

            // Iformfile
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.jpg";
            fileMock.Setup(_ => _.FileName).Returns(fileName);

            // Img Service
            var imgService = new Mock<IImageUploadService>();

            imgService.Setup(s => s.UploadArquivo(
                new ModelStateDictionary(),
                fileMock.Object,
                It.IsAny<string>()
                )).ReturnsAsync(true);

            // Controller
            var controller = new ProductsController(ctx, imgService.Object);

            var product = new Product()
            {
                Id = 1,
                Name = "Teclado",
                ImageUpload = fileMock.Object,
                Value = "40"

            };
            var result = controller.Index().Result;

            // Assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}