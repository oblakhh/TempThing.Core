using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TempThing.Core.Server.Models
{
  public class Measurement
  {
    public int Id { get; set; }

    [IgnoreDataMember]
    [Required]
    public Device Device { get; set; }

    [Required]
    public MeasurementUnit MeasurementUnit { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public decimal Value { get; set; }
  }
}