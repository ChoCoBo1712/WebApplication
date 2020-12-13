using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ApplicationDbContext: IdentityDbContext<EFUser, EFUserRole, int>
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
            
            modelBuilder.Entity<EFUserRole>().HasData(new EFUserRole()
            {
                Id = 1,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<EFUserRole>().HasData(new EFUserRole()
            {
                Id = 2,
                Name = "user",
                NormalizedName = "USER"
            });
            
            modelBuilder.Entity<EFUser>().HasData(new EFUser()
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "webappprog@gmail.com",
                NormalizedEmail = "WEBAPPPROG@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<EFUser>().HashPassword(null, "admin"),
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