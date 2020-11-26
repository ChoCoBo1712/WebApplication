using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserRole
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }
    }
}