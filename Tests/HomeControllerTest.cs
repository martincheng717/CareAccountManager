using NUnit.Framework;
using System.Web.Mvc;
using CareGateway.Controllers;

namespace Tests
{
    public class HomeControllerTest
    {
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
