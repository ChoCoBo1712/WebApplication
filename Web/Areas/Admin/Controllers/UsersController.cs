using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Interfaces;

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
        
        public IActionResult Index() => View(userRepository.GetAllUsers());
        
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