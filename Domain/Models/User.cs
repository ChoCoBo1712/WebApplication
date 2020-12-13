using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User: BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }
        
        [Required]
        public bool EmailConfirmed { get; set; }
        
        [Required]
        public string PasswordHash { get; set; }
    }
}