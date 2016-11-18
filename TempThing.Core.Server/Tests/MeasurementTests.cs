using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using TempThing.Core.Server.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace TempThing.Core.Server.Tests
{
    public class MeasurementTests
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
              .AddEntityFrameworkInMemoryDatabase()
              .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder
              .UseInMemoryDatabase()
              .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public void Add_writes_to_database()
        {
            // All contexts that share the same service provider will share the same InMemory database
            var options = CreateNewContextOptions();

            // Insert seed data into the database using one instance of the context
            using (var baseUnitsContext = new ApplicationDbContext(options))
            {
                baseUnitsContext.MeasurementUnits.Add(new MeasurementUnit
                {
                    Id = "tempc",
                    Name = "Temperature (C)",
                    Format = "{0:0.00} °C"
                });


                baseUnitsContext.Devices.Add(new Device
                {
                    Name = "Home",
                    Description = "Temperature only"
                });

                baseUnitsContext.SaveChanges();
            }

            using (var measurementsContext = new ApplicationDbContext(options))
            {
                var degreesC = (from mu in measurementsContext.MeasurementUnits where mu.Name == "Temperature (C)" select mu).FirstOrDefault();
                Assert.NotNull(degreesC);

                var home = (from dv in measurementsContext.Devices where dv.Name == "Home" select dv).FirstOrDefault();
                Assert.NotNull(home);

                measurementsContext.Measurements.Add(new Measurement
                {
                    Value = 23.87M,
                    Created = DateTime.Now.AddMinutes(-1),
                    Device = home,
                    MeasurementUnit = degreesC
                });

                measurementsContext.Measurements.Add(new Measurement
                {
                    Value = 22M,
                    Created = DateTime.Now,
                    Device = home,
                    MeasurementUnit = degreesC
                });

                measurementsContext.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var rcontext = new ApplicationDbContext(options))
            {
                var result = from meas in rcontext.Measurements where meas.Device.Name == "Home" select meas;
                Assert.Equal(2, result.Count());
            }
        }

    }
}