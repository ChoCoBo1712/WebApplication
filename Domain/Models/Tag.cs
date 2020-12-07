using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Tag: BaseEntity
    {
        [Required] public string Name { get; set; }

        [Required] public int UserId { get; set; }
        
        [Required] public bool Verified { get; set; }

        public List<Song> Songs { get; set; }
    }
}