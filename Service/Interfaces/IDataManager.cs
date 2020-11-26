using Domain;
using Domain.Models;

namespace Service.Interfaces
{
    public interface IDataManager
    {
        IRepository<Song> SongRepository { get; set; }
        IRepository<Album> AlbumRepository { get; set; }
        IRepository<Artist> ArtistRepository { get; set; }
        IRepository<Tag> TagRepository { get; set; }
    }
}