using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Repository;
using Service.Implementations;
using Service.Interfaces;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Web.Configs
{
    public static class DIExtensions
    {
        public static void AddAuth(this IServiceCollection services, IConfiguration config)
        {
            var googleConfig = new GoogleConfig();
            config.GetSection(GoogleConfig.SectionName).Bind(googleConfig);

            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = googleConfig.ClientId;
                options.ClientSecret = googleConfig.ClientSecret;
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

        public static void AddAppServices(this IServiceCollection services, IWebHostEnvironment env,
            IConfiguration config)
        {
            var mailConfig = new MailConfig();
            var loggingConfig = new LoggingConfig();
            config.GetSection(MailConfig.SectionName).Bind(mailConfig);
            config.GetSection(LoggingConfig.SectionName).Bind(loggingConfig);
            
            services.AddLogging(opt =>
            {
                opt.AddConsole();
                opt.AddFile(Path.Combine(env.WebRootPath, loggingConfig.CommonLogFilePath));
                opt.AddFile(Path.Combine(env.WebRootPath, loggingConfig.ErrorLogFilePath), LogLevel.Error);
            });
            
            services.AddScoped<IDataManager, DataManager>();
            
            services.AddScoped<IFileService, FileService>();

            services.AddScoped<ISearchService>(t => new SearchService(t.GetRequiredService<IDataManager>()));
            
            services.AddControllersWithViews();
            
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            
            services.AddSingleton(typeof(IMapper), mapperConfig.CreateMapper());
            
            services.AddScoped<IEmailService>(t => new EmailService(
                MailConfig.Sender, MailConfig.SmtpServer, MailConfig.SmtpPort, MailConfig.Username, MailConfig.Password
            ));
            
            services.AddSignalR();
        }
    }
}