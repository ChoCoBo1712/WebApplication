using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<EFSong> Songs { get; set; }
        public DbSet<EFArtist> Artists { get; set; }
        public DbSet<EFAlbum> Albums { get; set; }
        public DbSet<EFTag> Tags { get; set; }
        public DbSet<EFUser> Users { get; set; }
    }
}