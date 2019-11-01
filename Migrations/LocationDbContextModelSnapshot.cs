using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MicroserviceCore.LocationService.Persistence;

namespace MicroserviceCore.LocationService.Migrations
{
    [DbContext(typeof(LocationDbContext))]
    partial class LocationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:.uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("MicroserviceCore.LocationService.Models.LocationRecord", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Altitude");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<Guid>("MemberID");

                    b.Property<long>("Timestamp");

                    b.HasKey("ID");

                    b.ToTable("LocationRecords");
                });
        }
    }
}