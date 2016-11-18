using System.ComponentModel.DataAnnotations;

namespace TempThing.Core.Server.Models
{
  ///<summary>
  /// A measurement unit
  ///</summary>
  public class MeasurementUnit
  {
    public string Id { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 1)]
    public string Name { get; set; }

    [Required]
    public string Format { get; set; }
  }
}