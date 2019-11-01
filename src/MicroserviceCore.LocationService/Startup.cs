using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicroserviceCore.LocationService.Models;
using MicroserviceCore.LocationService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroserviceCore.LocationService
{
    public class Startup
    {
        public static string[] Args { get; set; } = new string[] { };
        private ILogger logger;
        private ILoggerFactory loggerFactory;

        public Startup(IHostingEnvironment evironment, ILoggerFactory loggerFactory)
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true)
                                    .AddEnvironmentVariables()
                                    .AddCommandLine(Startup.Args);

            Configuration = builder.Build();

            this.loggerFactory = loggerFactory;
            loggerFactory.AddConsole(LogLevel.Information);
            this.loggerFactory.AddDebug();

            this.logger = this.loggerFactory.CreateLogger("Startup");
        }

        public static IConfigurationRoot Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var transient = true;

            if (Configuration.GetSection("transient") != null)
            {
                transient = Boolean.Parse(Configuration.GetSection("transient").Value);
            }

            if (transient)
            {
                logger.LogInformation("Using transient location record repository");
                services.AddScoped<ILocationRecordRepository, InMemoryLocationRecordRepository>();
            }
            else
            {
                var connectionString = Configuration.GetSection("postgres:cstr").Value;

                services.AddEntityFrameworkNpgsql().AddDbContext<LocationDbContext>(options => options.UseNpgsql(connectionString));

                logger.LogInformation("Using '{0}' for DB connection string", connectionString);

                services.AddScoped<ILocationRecordRepository, LocationRecordRepository>();
            }

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}