using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BaseEntity
    {
        [Required] public int Id { get; set; }
    }
}