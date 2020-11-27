using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class SongRepository : IRepository<Song>
    {  
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public SongRepository(ApplicationDbContext context, IMapper mapper)  
        {  
            this.context = context;
            this.mapper = mapper;
        }  
        
        public List<Song> GetAll()  
        {  
            var efSongs = context.Songs.ToList();
            var songs = mapper.Map<List<Song>>(efSongs);
            foreach (var pair in efSongs.Zip(songs, 
                (efSong, song) => new { EFSong = efSong, Song = song }))
            {
                pair.Song.Album = mapper.Map<Album>(context.Albums.FirstOrDefault(t => t.Id == pair.EFSong.Album.Id));
                pair.Song.Tags = mapper.Map<List<Tag>>(context.Tags.Where(t => t.Id == pair.EFSong.Id));
            }

            return songs;
        }  
        
        public Song Get(int id)  
        {  
            var efSong = context.Songs.FirstOrDefault(t => t.Id == id);

            if (efSong == null)
                return null;
            
            var song = mapper.Map<Song>(efSong);
            song.Album = mapper.Map<Album>(context.Albums.FirstOrDefault(t => t.Id == efSong.AlbumId));
            song.Tags = mapper.Map<List<Tag>>(context.Tags.Where(t => t.Id == efSong.Id));

            return song;
        }

        public int Save(Song song)
        {
            var efSong = mapper.Map<Song>(song);
            efSong.Album = mapper.Map<Album>(context.Albums.FirstOrDefault(t => t.Id == song.Album.Id));
            efSong.Tags = mapper.Map<List<Tag>>(context.Tags.Where(t => t.Id == song.Id));
            context.Entry(efSong).State = efSong.Id == default ? EntityState.Added : EntityState.Modified;
            context.SaveChanges();
            
            return efSong.Id;
        }

        public void Delete(int id)
        {
            var efSong = context.Songs.FirstOrDefault(t => t.Id == id);

            if (efSong != null)
            {
                context.Songs.Remove(efSong);
            }

            context.SaveChanges();
        }
    }
}