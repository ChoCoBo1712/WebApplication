using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.Interfaces;
using Web.Areas.Admin.ViewModels;
using Web.Hubs;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagsController : Controller
    {
        private IDataManager dataManager;
        IHubContext<NotificationHub> hubContext;

        public TagsController(IDataManager dataManager, IHubContext<NotificationHub> hubContext)
        {
            this.dataManager = dataManager;
            this.hubContext = hubContext;
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
                    Name = model.Name.ToLower(), UserId = 1, Verified = true
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
                Id = tag.Id, Name = tag.Name
            };
            return View(model);
        }
 
        [HttpPost]
        public async Task<IActionResult> Edit(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tag tag = dataManager.TagRepository.Get(model.Id);
                if (tag != null)
                {
                    tag.Name = model.Name.ToLower();
                    if (!tag.Verified)
                    {
                        tag.Verified = true;
                        await hubContext.Clients.User(tag.UserId.ToString()).SendAsync("notify", "Your tag has been verified and added");
                    }

                    dataManager.TagRepository.Save(tag);
                    return Redirect("/admin/tags");
                }
            }
            return View(model);
        }
 
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            Tag tag = dataManager.TagRepository.Get(id);
            if (tag != null)
            {
                if (!tag.Verified)
                {
                    await hubContext.Clients.User(tag.UserId.ToString()).SendAsync("notify", "Your tag hasn't been verified and was deleted");
                }
                dataManager.TagRepository.Delete(id);
            }
            return Redirect("/admin/tags");
        }
    }
}