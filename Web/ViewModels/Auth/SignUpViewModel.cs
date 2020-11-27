using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Auth
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "First name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string Surname { get; set; }
        
        [Required]
        [Display(Name = "Password")]
        [StringLength(64, MinimumLength = 8, ErrorMessage = "Passwords must be at least 8 characters.")]
        public string Password { get; set; }
        
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        
        [Display(Name = "ConfirmEmail")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string ConfirmEmail { get; set; }
    }
}