using InstaAPI.Areas.HelpPage.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InstaAPI.Tests.Controllers
{
    [TestClass]
    public class HelpControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HelpController();

            // Act
            var result = controller.Response;

            // Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual(Helpers.ConfigurationData.TitleHelp, result.ViewBag.Title);
        }
    }
}
