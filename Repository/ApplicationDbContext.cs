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
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EFSong>()
                .HasOne(s => s.Album)
                .WithMany(a => a.Songs)
                .HasForeignKey(s => s.AlbumId);

            modelBuilder.Entity<EFSong>()
                .HasMany(s => s.Tags)
                .WithMany(t => t.Songs)
                .UsingEntity(b => b.ToTable("AddedTags"));
            
            modelBuilder.Entity<EFAlbum>()
                .HasOne(al => al.Artist)
                .WithMany(ar => ar.Albums)
                .HasForeignKey(al => al.ArtistId);
        }
    }
}