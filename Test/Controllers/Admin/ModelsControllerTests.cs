using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Controllers;
using Xunit;

namespace Test.Admin
{
    public class ModelsControllerTests
    {
        [Fact]
        public void IndexTest()
        {
            var controller = new ModelsController();

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
    }
}