using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Web.Areas.Admin.ViewModels;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SongsController : Controller
    {
        private IDataManager dataManager;

        public SongsController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult Index() => View(dataManager.SongRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            return View();
        }
 
        [HttpPost]
        public IActionResult Create(SongViewModel model)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            if (ModelState.IsValid)
            {
                List<Tag> tags = new List<Tag>();
                foreach (var id in model.TagIds)
                {
                    tags.Add(dataManager.TagRepository.Get(id));
                }
                Song song = new Song
                {
                    Name = model.Name, Album = dataManager.AlbumRepository.Get(model.AlbumId), Tags = tags
                };
                dataManager.SongRepository.Save(song);
                return Redirect("/admin/songs");
            }
            return View(model);
        }
 
        public IActionResult Edit(int id)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            Song song = dataManager.SongRepository.Get(id);
            if (song == null)
            {
                return NotFound();
            }
            List<int> tagIds = new List<int>();
            foreach (var tag in song.Tags)
            {
                tagIds.Add(tag.Id);
            }
            SongViewModel model = new SongViewModel
            {
                Id = song.Id, Name = song.Name, AlbumId = song.Album.Id, TagIds = tagIds
            };
            return View(model);
        }
 
        [HttpPost]
        public IActionResult Edit(SongViewModel model)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            if (ModelState.IsValid)
            {
                Song song = dataManager.SongRepository.Get(model.Id);
                if (song != null)
                {
                    List<Tag> tags = new List<Tag>();
                    foreach (var id in model.TagIds)
                    {
                        tags.Add(dataManager.TagRepository.Get(id));
                    }
                    song.Name = model.Name;
                    song.Album = dataManager.AlbumRepository.Get(model.AlbumId);
                    song.Tags = tags;

                    dataManager.SongRepository.Save(song);
                    return Redirect("/admin/songs");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Song song = dataManager.SongRepository.Get(id);
            if (song != null)
            {
                dataManager.SongRepository.Delete(id);
            }
            return Redirect("/admin/songs");
        }
    }
}