using System.Collections.Generic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Service.Interfaces;
using Web.Areas.Admin.Controllers;
using Web.Controllers;
using Web.Hubs;
using Web.ViewModels.Home;
using Xunit;

namespace Test.Admin
{
    public class UsersControllerTests
    {
        [Fact]
        public void IndexTest()
        {
            var controller = new ModelsController();

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void DeleteTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestUsers());
            var searchService = new Mock<ISearchService>();
            var controller = new HomeController(dataManager.Object, hubContext.Object, searchService.Object);

            ViewResult result = controller.Index() as ViewResult;
            
            var model = Assert.IsAssignableFrom<SearchViewModel>(result.Model);
            Assert.Equal(model, result.Model);
        }
        
        [Fact]
        public void DeleteNullTest()
        {
            var controller = new ModelsController();

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        public static List<Song> GetTestUsers()
        {
            return new List<Song>
            {
                new Song { Id = 1, Name = "1"},
                new Song {Id = 2, Name = "2"}
            };
        }
    }
}