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
    public class AlbumsController : Controller
    {
        private IDataManager dataManager;
        private IFileService fileService;
        private IWebHostEnvironment environment;

        public AlbumsController(IDataManager dataManager, IFileService fileService, IWebHostEnvironment environment)
        {
            this.dataManager = dataManager;
            this.fileService = fileService;
            this.environment = environment;
        }
        
        public IActionResult Index() => View(dataManager.AlbumRepository.GetAll());

        public IActionResult Create()
        {
            ViewBag.Artists = dataManager.ArtistRepository.GetAll();
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> Create(AlbumViewModel model)
        {
            ViewBag.Artists = dataManager.ArtistRepository.GetAll();
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.Image != null)
                {
                    fileName = await UploadFile(model.Image);
                } 
                Album album = new Album
                {
                    Name = model.Name, ImagePath = fileName, Artist = dataManager.ArtistRepository.Get(model.ArtistId)
                };
                dataManager.AlbumRepository.Save(album);
                return Redirect("/admin/albums");
            }
            return View(model);
        }
 
        public IActionResult Edit(int id)
        {
            ViewBag.Artists = dataManager.ArtistRepository.GetAll();
            Album album = dataManager.AlbumRepository.Get(id);
            if (album == null)
            {
                return NotFound();
            }
            AlbumViewModel model = new AlbumViewModel
            {
                Id = album.Id, Name = album.Name, ImagePath = album.ImagePath, ArtistId = album.Artist.Id
            };
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(AlbumViewModel model)
        {
            ViewBag.Artists = dataManager.ArtistRepository.GetAll();
            if (ModelState.IsValid)
            {
                Album album = dataManager.AlbumRepository.Get(model.Id);
                if (album != null)
                {
                    if (model.Image != null)
                    {
                        if (album.ImagePath != null)
                            DeleteFile(album.ImagePath);
                        album.ImagePath = await UploadFile(model.Image);
                    }
                    album.Name = model.Name;
                    album.Artist = dataManager.ArtistRepository.Get(model.ArtistId);

                    dataManager.AlbumRepository.Save(album);
                    return Redirect("/admin/albums");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Album album = dataManager.AlbumRepository.Get(id);
            if (album != null)
            {
                DeleteFile(album.ImagePath);
                dataManager.AlbumRepository.Delete(id);
            }
            return Redirect("/admin/albums");
        }
        
        private async Task<string> UploadFile(IFormFile file)
        {
            string uploadDir = Path.Combine(environment.WebRootPath, "img/albums");

            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }
            
            string fileName = $"{Guid.NewGuid().ToString()}_{file.FileName}";
            string filePath = Path.Combine(uploadDir, fileName);
            
            await fileService.SaveFile(file, filePath);
            return fileName;
        }

        private void DeleteFile(string fileName)
        {
            string uploadDir = Path.Combine(environment.WebRootPath, "img/albums");
            string filePath = Path.Combine(uploadDir, fileName);
            
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}