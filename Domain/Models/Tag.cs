using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Tag: BaseEntity
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Description { get; set; }
        
        [Required] public List<Song> Songs { get; set; }
    }
}