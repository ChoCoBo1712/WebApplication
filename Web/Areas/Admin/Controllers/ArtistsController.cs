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
    public class ArtistsController : Controller
    {
        private IDataManager dataManager;
        private IFileService fileService;
        private IWebHostEnvironment environment;

        public ArtistsController(IDataManager dataManager, IFileService fileService, IWebHostEnvironment environment)
        {
            this.dataManager = dataManager;
            this.fileService = fileService;
            this.environment = environment;
        }
        
        public IActionResult Index() => View(dataManager.ArtistRepository.GetAll());
        
        public IActionResult Create() => View();
 
        [HttpPost]
        public async Task<IActionResult> Create(ArtistViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (model.Image != null)
                {
                    fileName = await fileService.UploadFile(model.Image, Path.Combine(environment.WebRootPath, "img/artists"));
                } 
                
                if (fileName == null)
                {
                    ModelState.AddModelError(nameof(model.Image), "Choose an image");
                    return View(model);
                }
                
                Artist artist = new Artist { Name = model.Name, ImagePath = fileName, Description = model.Description };
                dataManager.ArtistRepository.Save(artist);
                return Redirect("/admin/artists");
            }
            return View(model);
        }
 
        public IActionResult Edit(int id)
        {
            Artist artist = dataManager.ArtistRepository.Get(id);
            if (artist == null)
            {
                return NotFound();
            }
            ArtistViewModel model = new ArtistViewModel
            {
                Id = artist.Id, Name = artist.Name, ImagePath = artist.ImagePath, Description = artist.Description
            };
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(ArtistViewModel model)
        {
            if (ModelState.IsValid)
            {
                Artist artist = dataManager.ArtistRepository.Get(model.Id);
                if (artist != null)
                {
                    if (model.Image != null)
                    {
                        if (artist.ImagePath != null)
                            fileService.DeleteFile(artist.ImagePath, Path.Combine(environment.WebRootPath, "img/artists"));
                        artist.ImagePath = await fileService.UploadFile(model.Image, Path.Combine(environment.WebRootPath, "img/artists"));
                    }
                    artist.Name = model.Name;
                    artist.Description = model.Description;

                    dataManager.ArtistRepository.Save(artist);
                    return Redirect("/admin/artists");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Artist artist = dataManager.ArtistRepository.Get(id);
            if (artist != null)
            {
                fileService.DeleteFile(artist.ImagePath, Path.Combine(environment.WebRootPath, "img/artists"));
                dataManager.ArtistRepository.Delete(id);
            }
            return Redirect("/admin/artists");
        }
    }
}