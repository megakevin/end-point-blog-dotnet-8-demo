using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Core.Validation;

namespace VehicleQuotes.Core.Models;

[Index(nameof(Name), IsUnique = true)]
[UniqueMakeName]
public class Make
{
    public int ID { get; set; }
    public required string Name { get; set; }
}
