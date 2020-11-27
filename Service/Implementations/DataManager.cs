using Domain;
using Domain.Models;
using Service.Interfaces;

namespace Service.Implementations
{
    public class DataManager : IDataManager
    {
        public IRepository<Song> SongRepository { get; set; }
        public IRepository<Album> AlbumRepository { get; set; }
        public IRepository<Artist> ArtistRepository { get; set; }
        public IRepository<Tag> TagRepository { get; set; }

        public DataManager(IRepository<Song> SongRepository, 
            IRepository<Album> AlbumRepository,
            IRepository<Artist> ArtistRepository,
            IRepository<Tag> TagRepository)
        {
            this.SongRepository = SongRepository;
            this.AlbumRepository = AlbumRepository;
            this.ArtistRepository = ArtistRepository;
            this.TagRepository = TagRepository;
        }
    }
}