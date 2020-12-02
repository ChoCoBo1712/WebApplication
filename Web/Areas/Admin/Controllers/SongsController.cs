using System;
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
            return View();
        }
 
        [HttpPost]
        public IActionResult Create(SongViewModel model)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            if (ModelState.IsValid)
            {
                Song song = new Song
                {
                    Name = model.Name, Album = dataManager.AlbumRepository.Get(model.AlbumId)
                };
                dataManager.SongRepository.Save(song);
                return Redirect("/admin/songs");
            }
            return View(model);
        }
 
        public IActionResult Edit(int id)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            Song song = dataManager.SongRepository.Get(id);
            if (song == null)
            {
                return NotFound();
            }
            SongViewModel model = new SongViewModel
            {
                Id = song.Id, Name = song.Name, AlbumId = song.Album.Id
            };
            return View(model);
        }
 
        [HttpPost]
        public IActionResult Edit(SongViewModel model)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            if (ModelState.IsValid)
            {
                Song song = dataManager.SongRepository.Get(model.Id);
                if (song != null)
                {
                    song.Name = model.Name;
                    song.Album = dataManager.AlbumRepository.Get(model.AlbumId);

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