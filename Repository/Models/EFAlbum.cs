using System.Collections.Generic;

namespace Repository.Models
{
    public class EFAlbum
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string ImagePath { get; set; }
        
        public EFArtist Artist { get; set; }
        
        public int ArtistId { get; set; }
        
        public List<EFSong> Songs { get; set; }
    }
}