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
using DbContextOptions = Repository.DbContextOptions;

namespace Web
{
    public class Startup
    {
        private IConfiguration configuration { get; }
        private IWebHostEnvironment env { get; }
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        } 
        
        public void ConfigureServices(IServiceCollection services)
        {
            var appOptions = new AppConfig();
            configuration.GetSection(AppConfig.SectionName).Bind(appOptions);
            
            //DB and repos
            services.AddRepositories(new DbContextOptions { ConnectionString = appOptions.ConnectionString });

            //Data and services
            services.AddAppServices(env, configuration);

            //Authentication
            services.AddAuth(configuration);
            
            services.ConfigureIdentity(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            });
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