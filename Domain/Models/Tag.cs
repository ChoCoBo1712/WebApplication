using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Tag
    {
        [Required] public int Id { get; set; }
        
        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }
    }
}