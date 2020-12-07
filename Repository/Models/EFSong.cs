using System.Collections.Generic;

namespace Repository.Models
{
    public class EFSong
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EFAlbum Album { get; set; }
        
        public int AlbumId { get; set; }
        
        public string FilePath { get; set; }

        public List<EFTag> Tags { get; set; }
    }
}