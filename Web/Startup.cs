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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Models;
using Service.Implementations;
using Service.Interfaces;
using Web.Configs;

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
            //DB and configs
            Configuration.Bind("Project", new Config());
            Configuration.Bind("Mailing", new MailConfig());
            Configuration.Bind("Google", new GoogleConfig());
            services.AddDbContext<ApplicationDbContext>(t => 
                t.UseNpgsql(Config.ConnectionString), ServiceLifetime.Transient);
            
            //Data and services
            services.AddScoped<IRepository<Song>, SongRepository>();
            services.AddScoped<IRepository<Album>, AlbumRepository>();
            services.AddScoped<IRepository<Artist>, ArtistRepository>();
            services.AddScoped<IRepository<Tag>, TagRepository>();
            services.AddScoped<IDataManager, DataManager>();
            services.AddScoped<IImageService, ImageService>();
            services.AddControllersWithViews();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            services.AddSingleton(typeof(IMapper), mapperConfig.CreateMapper());
            services.AddScoped<IPopulator>(t => new Populator(t.GetRequiredService<IDataManager>()));
            services.AddScoped<IEmailService>(t => new EmailService(
                MailConfig.Sender, MailConfig.SmtpServer, MailConfig.SmtpPort, MailConfig.Username, MailConfig.Password
            ));
            
            //Authentication
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = GoogleConfig.ClientId;
                options.ClientSecret = GoogleConfig.ClientSecret;
            });
            // .AddUserStore<UserStore<EFUser, EFUserRole, ApplicationDbContext, int>>()
            // .AddRoleStore<RoleStore<EFUserRole, ApplicationDbContext, int>>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();
 
            app.UseRouting();
 
            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}");
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=models}/{action=index}");
                // endpoints.MapControllerRoute("default", "{controller=account}/{action=register}");
            });
        }
    }
}