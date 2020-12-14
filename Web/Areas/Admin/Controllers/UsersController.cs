using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.ViewModels;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(userRepository.GetAllUsers());
        }
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userRepository.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await userRepository.DeleteAsync(user);
            }
            return Redirect("/admin/users");
        }
    }
}