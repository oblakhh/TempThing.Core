using Microsoft.EntityFrameworkCore;

namespace TempThing.Core.Server.Models
{
  public class ApplicationDbContext : DbContext
  {
    public static void Migrate()
    {
      using (var context = new ApplicationDbContext())
      {
        context.Database.Migrate();
      }
    }

    private ApplicationDbContext() {}

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {
    }

    public DbSet<Measurement> Measurements { get; set; }

    public DbSet<MeasurementUnit> MeasurementUnits { get; set; }

    public DbSet<Device> Devices { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite("Data Source=/Data/Measurement.db");
    }
  }
}