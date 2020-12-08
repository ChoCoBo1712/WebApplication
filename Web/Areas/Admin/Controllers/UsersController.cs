using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<IdentityUser<int>> userManager;

        public UsersController(UserManager<IdentityUser<int>> userManager)
        {
            this.userManager = userManager;
        }
        
        public IActionResult Index() => View(userManager.Users.ToList());
        
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return Redirect("/admin/users");
        }
    }
}