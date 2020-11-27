using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Service.Implementations;
using Service.Interfaces;
using Web.Services;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        } 
        
        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());
            services.AddDbContext<ApplicationDbContext>(x => 
                x.UseNpgsql(Config.ConnectionString), ServiceLifetime.Transient);
            services.AddScoped<IRepository<Song>, SongRepository>();
            // services.AddScoped<IRepository<Album>, AlbumRepository>();
            // services.AddScoped<IRepository<Artist>, ArtistRepository>();
            // services.AddScoped<IRepository<Tag>, TagRepository>();
            services.AddScoped<IDataManager, DataManager>();
            services.AddControllersWithViews();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(typeof(IMapper), mapperConfig.CreateMapper());
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}");
            });
        }
    }
}