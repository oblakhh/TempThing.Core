using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TempThing.Core.Server.Models;

namespace TempThing.Core.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("TempThing.Core.Server.Models.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("Password")
                        .HasAnnotation("MaxLength", 32);

                    b.HasKey("Id");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("TempThing.Core.Server.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<int?>("DeviceId")
                        .IsRequired();

                    b.Property<string>("MeasurementUnitId")
                        .IsRequired();

                    b.Property<decimal>("Value");

                    b.HasKey("Id");

                    b.HasIndex("DeviceId");

                    b.HasIndex("MeasurementUnitId");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("TempThing.Core.Server.Models.MeasurementUnit", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Format")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.HasKey("Id");

                    b.ToTable("MeasurementUnits");
                });

            modelBuilder.Entity("TempThing.Core.Server.Models.Measurement", b =>
                {
                    b.HasOne("TempThing.Core.Server.Models.Device", "Device")
                        .WithMany()
                        .HasForeignKey("DeviceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TempThing.Core.Server.Models.MeasurementUnit", "MeasurementUnit")
                        .WithMany()
                        .HasForeignKey("MeasurementUnitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
