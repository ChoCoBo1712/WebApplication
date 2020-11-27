using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Album: BaseEntity
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Picture { get; set; }
        
        [Required] public Artist Artist { get; set; }

        public List<Song> Songs { get; set; }
    }
}