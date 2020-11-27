using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Artist: BaseEntity
    {
        [Required] public string Name { get; set; }
        
        [Required] public string Picture { get; set; }
        
        [Required] public string Description { get; set; }
        
        public List<Album> Albums { get; set; }
    }
}