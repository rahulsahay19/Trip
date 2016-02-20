using AutoMapper;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json.Serialization;
using WorldTrip.Models;
using WorldTrip.Services;
using WorldTrip.ViewModels;

namespace WorldTrip
{
    public class Startup
    {
        public static IConfigurationRoot ConfigurationBuilder;
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            ConfigurationBuilder = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().
                AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ContractResolver= new CamelCasePropertyNamesContractResolver();
                });

            services.AddLogging();
            //Adding EF
            services.AddEntityFramework()
             .AddSqlServer()
        .AddDbContext<TripContext>();

            //ADDED DI here
            services.AddTransient<TripContextSeedData>();
            services.AddScoped<ITripRepository, TripRepository>();
#if DEBUG
            services.AddScoped<IMailService, MockMailService>();
#else
            services.AddScoped<IMailService, MailService>();
#endif
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, TripContextSeedData seeder, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddDebug(LogLevel.Information);

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
            });

            app.UseMvc(config =>
            {
                config.MapRoute(
                  name: "Default",
                  template: "{controller}/{action}/{id?}",
                  defaults: new { controller = "App", action = "Index" }
                  );
            });

            seeder.SeedData();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
