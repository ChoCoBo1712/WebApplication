using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Web.Areas.Admin.ViewModels;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private IDataManager dataManager;

        public TagsController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult Index() => View(dataManager.TagRepository.GetAll());

        public IActionResult Create()
        {
            return View();
        }
 
        [HttpPost]
        public IActionResult Create(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = new Tag
                {
                    Name = model.Name, Description = model.Description
                };
                dataManager.TagRepository.Save(tag);
                return Redirect("/admin/tags");
            }
            return View(model);
        }
 
        public IActionResult Edit(int id)
        {
            Tag tag = dataManager.TagRepository.Get(id);
            if (tag == null)
            {
                return NotFound();
            }
            TagViewModel model = new TagViewModel
            {
                Id = tag.Id, Name = tag.Name, Description = tag.Description
            };
            return View(model);
        }
 
        [HttpPost]
        public IActionResult Edit(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = dataManager.TagRepository.Get(model.Id);
                if (tag != null)
                {
                    tag.Name = model.Name;
                    tag.Description = model.Description;

                    dataManager.TagRepository.Save(tag);
                    return Redirect("/admin/tags");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Tag tag = dataManager.TagRepository.Get(id);
            if (tag != null)
            {
                dataManager.TagRepository.Delete(id);
            }
            return Redirect("/admin/tags");
        }
    }
}