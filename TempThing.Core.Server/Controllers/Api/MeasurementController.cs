using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TempThing.Core.Server.Models;

namespace TempThing.Core.Server.Controllers.Api
{
  [Route("api/measurement")]
  public class MeasurementController
  {
    protected ApplicationDbContext ApplicationDbContext;

    public MeasurementController(ApplicationDbContext applicationDbContext)
    {
      this.ApplicationDbContext = applicationDbContext;
    }

    [Route("{deviceId:int}")]
    [HttpPost]
    public ObjectResult Add(int deviceId, [FromBody] AddMeasurementCommand measurement)
    {
      if(measurement == null) {
        throw new Exception("Invalid data supplied");
      }

      var device = (from dv in ApplicationDbContext.Devices where dv.Id == deviceId select dv).FirstOrDefault();
       
      if(device == null) {
        throw new Exception("Device not found");
      }

      var measurementUnit = (from mu in ApplicationDbContext.MeasurementUnits where mu.Id ==  measurement.Unit select mu).FirstOrDefault();
       
      if(measurementUnit == null) {
        throw new Exception("Measurement unit not found");
      }

      var measurementEntity = new Measurement {
        Device = device,
        MeasurementUnit = measurementUnit,
        Created = DateTime.Now,
        Value = measurement.Value
      };

      ApplicationDbContext.Measurements.Add(measurementEntity);
      ApplicationDbContext.SaveChanges();
      return new ObjectResult(measurementEntity);
    }

    [Route("{deviceId:int}")]
    [HttpGet]
    public ObjectResult Get(int deviceId)
    {
      var result = ApplicationDbContext.Measurements.Include(m => m.MeasurementUnit).Where(ms => ms.Device.Id == deviceId);
      return new ObjectResult(result);
    }
  }
}