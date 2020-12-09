using System.Collections.Generic;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using Service.Interfaces;
using Web.Controllers;
using Web.Hubs;
using Web.ViewModels.Home;
using Xunit;

namespace Test
{
    public class AccountControllerTests
    {
        [Fact]
        public void LoginAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LoginNotAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LoginAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void LoginNotAuthenticatedPostSignInSuccessTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LoginNotAuthenticatedPostSignInFailTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void LogoutNotAuthenticatedTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void LogoutAuthenticatedTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void SignupAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SignupNotAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SignupAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void SignupNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void SignupNotAuthenticatedPostExternalSignupTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void SignupNotAuthenticatedPostDuplicateUsernameTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void SignupNotAuthenticatedPostDuplicateEmailTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void SignupNotAuthenticatedPostPasswordsDoNotMatchTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void SignupNotAuthenticatedPostEmailsDoNotMatchTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void ExternalSignupAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ExternalSignupNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalLoginAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalLoginNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalSignupCallbackAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ExternalSignupCallbackNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalSignupCallbackNotAuthenticatedPostNullExternalLoginInfoTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalLoginCallbackAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ExternalLoginCallbackNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalLoginCallbackNotAuthenticatedCannotLoginPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ExternalLoginCallbackNotAuthenticatedPostNullExternalLoginInfoTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ForgotPasswordNotAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ForgotPasswordAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ForgotPasswordAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ForgotPasswordNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ForgotPasswordNotAuthenticatedPostInvalidEmailTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ConfirmEmailAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ConfirmEmailNotAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ConfirmEmailAuthenticatedGetInvalidLinkTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ResetPasswordAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }
        
        [Fact]
        public void ResetPasswordNotAuthenticatedGetTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ResetPasswordNotAuthenticatedGetInvalidLinkTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ResetPasswordAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.TagRepository.GetAll()).Returns(GetTestTags());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            ViewResult result = controller.AddTag(1) as ViewResult;
            
            Assert.IsAssignableFrom<TagViewModel>(result?.Model);
        }

        [Fact]
        public void ResetPasswordNotAuthenticatedPostTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        [Fact]
        public void ResetPasswordNotAuthenticatedPostPasswordDoNotMatchTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void ResetPasswordNotAuthenticatedPostInvalidLinkTest()
        {
            var hubContext = new Mock<IHubContext<NotificationHub>>();
            var dataManager = new Mock<IDataManager>();
            dataManager.Setup(t => t.SongRepository.GetAll()).Returns(GetTestSongs());
            var controller = new HomeController(dataManager.Object, hubContext.Object);

            var result = controller.Index();
            
            Assert.IsType<ViewResult>(result);
        }
        
        public static List<Song> GetTestSongs()
        {
            return new List<Song>
            {
                new Song { Id = 1, Name = "1"},
                new Song {Id = 2, Name = "2"}
            };
        }
        
        public static Album GetTestAlbum()
        {
            return new Album {Id = 1, Name = "1"};
        }
        
        public static Artist GetTestArtist()
        {
            return new Artist {Id = 1, Name = "1"};
        }
        
        public static List<Tag> GetTestTags()
        {
            return new List<Tag>
            {
                new Tag { Id = 1, Name = "1"},
                new Tag {Id = 2, Name = "2"}
            };
        }
        
        public static Tag GetTestTag()
        {
            return new Tag {Id = 1, Name = "1"};
        }
    }
}