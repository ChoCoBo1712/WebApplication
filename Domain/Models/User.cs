using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User: BaseEntity
    {
        [Required] public string Username { get; set; }
        
        public string Name { get; set; }
        
        public string Surname { get; set; }
        
        [Required] public string Email { get; set; }
        
        [Required] public bool EmailConfirmed { get; set; }
        
        [Required] public string PasswordHash { get; set; }
        
        public string Userpic { get; set; }
    }
}