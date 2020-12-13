using System;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Models;

namespace Repository
{
    public static class DIExtensions
    {
        public static void AddRepositories(this IServiceCollection services, DbContextOptions options)
        {
            services.AddDbContext<ApplicationDbContext>(t => 
                t.UseNpgsql(options.ConnectionString), ServiceLifetime.Transient);
            services.AddScoped<IRepository<Song>, SongRepository>();
            services.AddScoped<IRepository<Album>, AlbumRepository>();
            services.AddScoped<IRepository<Artist>, ArtistRepository>();
            services.AddScoped<IRepository<Tag>, TagRepository>();
            services.AddScoped<IUserRepository>(x =>
                new UserRepository(
                    x.GetRequiredService<UserManager<EFUser>>(), 
                    x.GetRequiredService<RoleManager<EFUserRole>>(), 
                    x.GetRequiredService<SignInManager<EFUser>>(),
                    x.GetRequiredService<IMapper>()
                )
            );
        }

        public static void ConfigureIdentity(this IServiceCollection services, Action<IdentityOptions> options)
        {
            services.AddIdentity<EFUser, EFUserRole>(options)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}