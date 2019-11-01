using Microsoft.EntityFrameworkCore;
using MicroserviceCore.LocationService.Models;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;

namespace MicroserviceCore.LocationService.Persistence
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        public DbSet<LocationRecord> LocationRecords { get; set; }
    }

    public class LocationDbContextFactory : IDesignTimeDbContextFactory<LocationDbContext>
    {
        public LocationDbContext CreateDbContext(string[] args)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(new DirectoryInfo(Directory.GetCurrentDirectory()).FullName)
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LocationDbContext>();
            //var connectionString = Startup.Configuration.GetSection("postgres:csrt").Value;
            var connectionString = configuration.GetSection("postgres:cstr").Value;
            optionsBuilder.UseNpgsql(connectionString);
            return new LocationDbContext(optionsBuilder.Options);
        }
    }
}
