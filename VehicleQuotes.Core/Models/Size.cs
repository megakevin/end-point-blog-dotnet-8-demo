using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Core.Models;

[Index(nameof(Name), IsUnique = true)]
public class Size
{
    public int ID { get; set; }
    public required string Name { get; set; }
}
