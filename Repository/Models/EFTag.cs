using System.Collections.Generic;

namespace Repository.Models
{
    public class EFTag
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int UserId { get; set; }
        
        public bool Verified { get; set; }
        
        public List<EFSong> Songs { get; set; }
    }
}