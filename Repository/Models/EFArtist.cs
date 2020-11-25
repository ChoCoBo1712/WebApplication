using System.Collections.Generic;

namespace Repository.Models
{
    public class EFArtist
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Picture { get; set; }
        
        public string Description { get; set; }
        
        public List<EFAlbum> Albums { get; set; }
    }
}