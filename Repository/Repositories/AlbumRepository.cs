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
                pair.Album.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.AlbumId == pair.EFAlbum.Id));
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
            album.Songs = mapper.Map<List<Song>>(context.Songs.Where(t => t.AlbumId == efAlbum.Id));

            return album;
        }
    
        public int Save(Album album)
        {
            var efAlbum = mapper.Map<EFAlbum>(album);
            
            if (efAlbum.Id == default)
            {
                context.Entry(efAlbum).State = EntityState.Added;
            }
            else
            {
                var entry = context.Albums.First(t => t.Id == efAlbum.Id);
                context.Entry(entry).State = EntityState.Detached;
                context.Entry(efAlbum).State = EntityState.Modified;
            }
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