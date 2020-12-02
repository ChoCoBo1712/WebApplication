using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public DbSet<EFSong> Songs { get; set; }
        public DbSet<EFArtist> Artists { get; set; }
        public DbSet<EFAlbum> Albums { get; set; }
        public DbSet<EFTag> Tags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
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
            
            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>()
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole<int>>().HasData(new IdentityRole<int>()
            {
                Id = 2,
                Name = "customer",
                NormalizedName = "CUSTOMER"
            });
            
            modelBuilder.Entity<IdentityUser<int>>().HasData(new IdentityUser<int>()
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "webappprog@gmail.com",
                NormalizedEmail = "WEBAPPPROG@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(null, "admin"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>()
            {
                RoleId = 1,
                UserId = 1
            });
        }
    }
}