using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Core.Models;

[Index(nameof(Name), nameof(MakeID), IsUnique = true)]
public class Model
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public int MakeID { get; set; }

    public Make Make { get; set; } = default!;

    public ICollection<ModelStyle> ModelStyles { get; set; } = [];
}
