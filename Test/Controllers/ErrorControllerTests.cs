using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Xunit;

namespace Test
{
    public class ErrorControllerTests
    {
        [Fact]
        public void HttpStatusCodeHandlerGetTest403()
        {
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(403);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("403", viewResult.ViewName);
        }
        
        [Fact]
        public void HttpStatusCodeHandlerGetTest404()
        {
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(404);
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("404", viewResult.ViewName);
        }
        
        [Fact]
        public void HttpStatusCodeHandlerGetTestDefault()
        {
            var controller = new ErrorController();

            var result = controller.HttpStatusCodeHandler(0);
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("/", redirectResult.Url);
        }
    }
}