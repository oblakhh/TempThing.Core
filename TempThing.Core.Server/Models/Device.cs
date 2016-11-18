using System.ComponentModel.DataAnnotations;

namespace TempThing.Core.Server.Models
{
  public class Device
  {
    public int Id { get; set; }

    [Required]
    [StringLength(32, MinimumLength = 3)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [StringLength(32)]
    public string Password { get; set; }
  }
}