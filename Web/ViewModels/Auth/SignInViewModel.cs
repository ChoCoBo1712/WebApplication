using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Auth
{
    public class SignInViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}