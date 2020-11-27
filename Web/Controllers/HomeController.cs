using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataManager dataManager;
        private readonly IPopulator populator;

        public HomeController(IDataManager dataManager, IPopulator populator)
        {
            this.dataManager = dataManager;
            this.populator = populator;
        }
        
        public IActionResult Index()
        {
            // populator.Add();
            List<Song> songs = dataManager.SongRepository.GetAll();
            return View(songs);
        }
    }
}