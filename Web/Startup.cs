using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Models;
using Service.Implementations;
using Service.Interfaces;
using Web.Configs;
using Web.Hubs;

namespace Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment env { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
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
            services.AddScoped<IFileService, FileService>();
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
            services.AddSignalR();
            services.AddLogging(opt =>
            {
                opt.AddConsole();
                opt.AddFile(Path.Combine(env.WebRootPath, "logs/all.log"));
                opt.AddFile(Path.Combine(env.WebRootPath, "logs/error.log"), LogLevel.Error);
            });
            
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
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
            });
            services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
            });
            services.AddControllersWithViews(options =>
            {
                options.Conventions.Add(new AdminAreaAuth("Admin", "AdminArea"));
            })
            .AddSessionStateTempDataProvider();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithRedirects("/error/{0}");
            }
            
            app.UseStaticFiles();
            app.UseCookiePolicy();
 
            app.UseRouting();
 
            app.UseAuthentication();  
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}");
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=models}/{action=index}");
                endpoints.MapHub<NotificationHub>("/NotificationHub");
            });
        }
    }
}