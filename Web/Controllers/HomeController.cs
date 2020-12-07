using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Interfaces;
using Web.Areas.Admin.ViewModels;
using Web.Hubs;
using Web.ViewModels.Home;
using TagViewModel = Web.ViewModels.Home.TagViewModel;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataManager dataManager;
        private readonly IPopulator populator;
        IHubContext<NotificationHub> hubContext;

        public HomeController(IDataManager dataManager, IPopulator populator, IHubContext<NotificationHub> hubContext)
        {
            this.dataManager = dataManager;
            this.populator = populator;
            this.hubContext = hubContext;
        }
        
        public IActionResult Index()
        {
            SearchViewModel model = new SearchViewModel
            {
                Songs = dataManager.SongRepository.GetAll().OrderBy(t => t.Name).ToList(),
                Category = 0,
                Search = ""
            };
            // populator.Add();
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel model)
        {
            if (model.Search != null)
            {
                switch (model.Category)
                {
                    case 0:
                        model.Songs = dataManager.SongRepository.GetAll().Where(t => 
                            t.Name.Contains(model.Search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                        break;
                    case 1:
                        model.Songs = dataManager.SongRepository.GetAll().Where(t => 
                            t.Album.Name.Contains(model.Search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                        break;
                    case 2:
                        model.Songs = dataManager.SongRepository.GetAll().Where(t => 
                            t.Album.Artist.Name.Contains(model.Search, StringComparison.InvariantCultureIgnoreCase)).OrderBy(t => t.Name).ToList();
                        break;
                    case 3:
                        model.Songs = dataManager.SongRepository.GetAll().Where(t => 
                            t.Tags.Any(x => x.Name.Contains(model.Search, StringComparison.InvariantCultureIgnoreCase))).OrderBy(t => t.Name).ToList();
                        break;
                }
            }
            else
            {
                model.Songs = dataManager.SongRepository.GetAll().OrderBy(t => t.Name).ToList();
            }

            return View(model);
        }

        public IActionResult Album(int id)
        {
            Album album = dataManager.AlbumRepository.Get(id);
            return View(album);
        }

        public IActionResult Artist(int id)
        {
            Artist artist = dataManager.ArtistRepository.Get(id);
            return View(artist);
        }

        public IActionResult AddTag(int id)
        {
            TagViewModel model = new TagViewModel
            {
                SongId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTag(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = new Tag
                {
                    Name = model.Name, UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)), Verified = false
                };
                
                bool save = true;
                foreach (var element in dataManager.TagRepository.GetAll())
                {
                    if (element.Name == tag.Name)
                    {
                        tag = element;
                        save = false;
                    }
                }

                if (save)
                {
                    tag.Id = dataManager.TagRepository.Save(tag);
                }
                
                var song = dataManager.SongRepository.Get(model.SongId);
                var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!song.Tags.Any(t => t.Name == tag.Name))
                {
                    song.Tags.Add(tag);
                    dataManager.SongRepository.Save(song);
                    if (!save)
                    {
                        await hubContext.Clients.User(id).SendAsync("notify", "Your tag is already verified and was added");   
                    }
                }
                else
                {
                    await hubContext.Clients.User(id).SendAsync("notify", "This song already has your tag");
                }
                
                return Redirect("/");
            }
            return View(model);
        }
    }
}