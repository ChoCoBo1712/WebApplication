using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Username { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public string Surname { get; set; }
        
        [Required] public string Email { get; set; }
        
        [Required] public bool EmailConfirmed { get; set; }
        
        [Required] public string PasswordHash { get; set; }
        
        [Required] public string Userpic { get; set; }
    }
}