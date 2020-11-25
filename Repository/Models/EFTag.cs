using System.Collections.Generic;

namespace Repository.Models
{
    public class EFTag
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public List<EFSong> Songs { get; set; }
    }
}