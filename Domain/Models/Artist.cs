using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Artist
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public string Picture { get; set; }
        
        [Required] public string Description { get; set; }
        
        [Required] public List<Album> Albums { get; set; }
    }
}