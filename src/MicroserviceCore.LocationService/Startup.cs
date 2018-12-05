using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicroserviceCore.LocationService.Models;
using MicroserviceCore.LocationService.Persistence;

namespace MicroserviceCore.LocationService
{
    public class Startup
    {
        public static string[] Args {get; set;} = new string[] {};
        private ILogger logger;
        private ILoggerFactory loggerFactory;
        public IConfigurationRoot Configuration {get;}

        public Startup(IHostingEnvironment evironment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                                    .AddEnvironmentVariables()
                                    .AddCommandLine(Startup.Args);
            
            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}