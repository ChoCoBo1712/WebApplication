using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Album
    {
        [Required] public int Id { get; set; }

        [Required] public string Name { get; set; }
        
        [Required] public string Picture { get; set; }
        
        [Required] public string Artist { get; set; }
        
        [Required] public List<Song> Songs { get; set; }
    }
}