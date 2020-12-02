using System.Net.Sockets;
using Domain.Models;
using Repository;
using Repository.Models;
using Service.Interfaces;

namespace Service.Implementations
{
    public class Populator : IPopulator
    {
        private IDataManager manager;
        
        public Populator(IDataManager manager)
        {
            this.manager = manager;
        }
        
        public void Add()
        {
            Artist artist = new Artist
            {
                Name = "PerformanceArtist",
                Description = "Are not ashamed of our dicks",
                ImagePath = "*shit*"
            };
            int artistId = manager.ArtistRepository.Save(artist);

            Album album = new Album
            {
                Name = "GACHI",
                ImagePath = "*dick*",
                Artist = manager.ArtistRepository.Get(artistId)
            };
            int albumId = manager.AlbumRepository.Save(album);
            
            Song billy = new Song
            {
                Name = "Billy",
                Album = manager.AlbumRepository.Get(albumId)
            };
            manager.SongRepository.Save(billy);
            
            Song van = new Song
            {
                Name = "Van",
                Album = manager.AlbumRepository.Get(albumId) 
            };
            manager.SongRepository.Save(van);
        }
    }
}