using Catalog.Controllers;
using Catalog.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

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
            var context = new Mock<AppDbContext>();
            var controller = new ProductsController(context.Object);
            //Act
            var result = controller.Index().Result;
            //Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}