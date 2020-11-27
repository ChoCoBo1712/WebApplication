using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ArtistRepository : IRepository<Artist>
    {  
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ArtistRepository(ApplicationDbContext context, IMapper mapper)  
        {  
            this.context = context;
            this.mapper = mapper;
        }  
        
        public List<Artist> GetAll()  
        {  
            var efArtist = context.Artists.ToList();
            var artist = mapper.Map<List<Artist>>(efArtist);
            foreach (var pair in efArtist.Zip(artist, 
                (efArtist, artist) => new { EFArtist = efArtist, Artist = artist }))
            {
                pair.Artist.Albums = mapper.Map<List<Album>>(context.Albums.Where(t => t.Id == pair.EFArtist.Id));
            }

            return artist; 
        }  
        
        public Artist Get(int id)  
        {  
            var efArtist = context.Artists.FirstOrDefault(t => t.Id == id);

            if (efArtist == null)
                return null;
            
            var artist = mapper.Map<Artist>(efArtist);
            artist.Albums = mapper.Map<List<Album>>(context.Albums.Where(t => t.Id == efArtist.Id));

            return artist;
        }
    
        public int Save(Artist artist)
        {
            var efArtist = mapper.Map<EFArtist>(artist);
            efArtist.Albums= mapper.Map<List<EFAlbum>>(context.Artists.Where(t => t.Id == artist.Id));
            context.Entry(efArtist).State = efArtist.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            
            return efArtist.Id;
        }
    
        public void Delete(int id)
        {
            var efArtist = context.Artists.FirstOrDefault(t => t.Id == id);

            if (efArtist != null)
            {
                context.Artists.Remove(efArtist);
            }

            context.SaveChanges();
        }
    }
}