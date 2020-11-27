using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class AlbumRepository : IRepository<Album>
    {  
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AlbumRepository(ApplicationDbContext context, IMapper mapper)  
        {  
            this.context = context;
            this.mapper = mapper;
        }  
        
        public List<Album> GetAll()  
        {  
            var efAlbums = context.Albums.ToList();
            var albums = mapper.Map<List<Album>>(efAlbums);
            foreach (var pair in efAlbums.Zip(albums, 
                (efAlbum, album) => new { EFAlbum = efAlbum, Album = album }))
            {
                pair.Album.Artist = mapper.Map<Artist>(context.Artists.FirstOrDefault(t => t.Id == pair.EFAlbum.ArtistId));
                pair.Album.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == pair.EFAlbum.Id));
            }

            return albums;
        }  
        
        public Album Get(int id)  
        {  
            var efAlbum = context.Albums.FirstOrDefault(t => t.Id == id);

            if (efAlbum == null)
                return null;
            
            var album = mapper.Map<Album>(efAlbum);
            album.Artist = mapper.Map<Artist>(context.Artists.FirstOrDefault(t => t.Id == efAlbum.ArtistId));
            album.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.Id == efAlbum.Id));

            return album;
        }
    
        public int Save(Album album)
        {
            var efAlbum = mapper.Map<EFAlbum>(album);
            efAlbum.Artist = mapper.Map<EFArtist>(context.Artists.FirstOrDefault(t => t.Id == album.Id));
            efAlbum.Songs = mapper.Map<List<EFSong>>(context.Songs.Where(t => t.Id == album.Id));
            efAlbum.ArtistId = album.Artist.Id;
            context.Entry(efAlbum).State = efAlbum.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            
            return efAlbum.Id;
        }
    
        public void Delete(int id)
        {
            var efAlbum = context.Albums.FirstOrDefault(t => t.Id == id);

            if (efAlbum != null)
            {
                context.Albums.Remove(efAlbum);
            }

            context.SaveChanges();
        }
    }
}