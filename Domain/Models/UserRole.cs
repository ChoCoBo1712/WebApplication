using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class UserRole: BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}