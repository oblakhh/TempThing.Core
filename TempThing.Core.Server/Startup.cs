using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TempThing.Core.Server.Models;

namespace TempThing.Core.Server
{
  public class Startup
  {
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlite("Filename=Measurement.db")
      );
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseMvc();

      ApplicationDbContext.Migrate();
    }
  }
}