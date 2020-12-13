using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Song: BaseEntity
    {
        [Required] public string Name { get; set; }

        [Required] public Album Album { get; set; }
        
        [Required] public string FilePath { get; set; }

        [Required] public List<Tag> Tags { get; set; }
    }
}