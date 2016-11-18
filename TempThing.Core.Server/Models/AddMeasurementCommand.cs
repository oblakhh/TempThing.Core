using System.ComponentModel.DataAnnotations;

namespace TempThing.Core.Server.Models
{
  public class AddMeasurementCommand
  {
    [Required]
    public string Unit { get; set; }

    [Required]
    public decimal Value { get; set; }
  }
}