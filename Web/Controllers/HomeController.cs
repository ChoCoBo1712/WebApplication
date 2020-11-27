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

        public HomeController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        
        public IActionResult Index()
        {
            List<Song> songs = dataManager.SongRepository.GetAll();
            return View(songs);
        }
    }
}