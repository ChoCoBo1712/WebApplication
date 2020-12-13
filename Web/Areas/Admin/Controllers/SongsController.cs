using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Web.Areas.Admin.ViewModels;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SongsController : Controller
    {
        private IDataManager dataManager;
        private IFileService fileService;
        private IWebHostEnvironment environment;

        public SongsController(IDataManager dataManager, IFileService fileService, IWebHostEnvironment environment)
        {
            this.dataManager = dataManager;
            this.fileService = fileService;
            this.environment = environment;
        }
        
        public IActionResult Index() => View(dataManager.SongRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            return View();
        }
 
        [HttpPost]
        public async Task <IActionResult> Create(SongViewModel model)
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
                string fileName = null;
                if (model.File != null)
                {
                    fileName = await fileService.UploadFile(model.File, Path.Combine(environment.WebRootPath, "audio"));
                }
                
                if (fileName == null)
                {
                    ModelState.AddModelError(nameof(model.File), "Choose a file");
                    return View(model);
                }
                
                Song song = new Song
                {
                    Name = model.Name, Album = dataManager.AlbumRepository.Get(model.AlbumId), Tags = tags, FilePath = fileName
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
        public async Task<IActionResult> Edit(SongViewModel model)
        {
            ViewBag.Albums = dataManager.AlbumRepository.GetAll();
            ViewBag.Tags = dataManager.TagRepository.GetAll();
            if (ModelState.IsValid)
            {
                Song song = dataManager.SongRepository.Get(model.Id);
                if (song != null)
                {
                    song.Tags.Clear();
                    List<Tag> tags = new List<Tag>();
                    foreach (var id in model.TagIds)
                    {
                        tags.Add(dataManager.TagRepository.Get(id));
                    }
                    if (model.File != null)
                    {
                        if (song.FilePath != null)
                            fileService.DeleteFile(song.FilePath, Path.Combine(environment.WebRootPath, "audio"));
                        song.FilePath = await fileService.UploadFile(model.File, Path.Combine(environment.WebRootPath, "audio"));
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
                fileService.DeleteFile(song.FilePath, Path.Combine(environment.WebRootPath, "audio"));
                dataManager.SongRepository.Delete(id);
            }
            return Redirect("/admin/songs");
        }
    }
}