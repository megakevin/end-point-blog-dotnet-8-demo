using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Core.Models;

[Index(nameof(ModelID), nameof(BodyTypeID), nameof(SizeID), IsUnique = true)]
public class ModelStyle
{
    public int ID { get; set; }
    public int ModelID { get; set; }
    public int BodyTypeID { get; set; }
    public int SizeID { get; set; }

    public Model Model { get; set; } = default!;
    public BodyType BodyType { get; set; } = default!;
    public Size Size { get; set; } = default!;

    public ICollection<ModelStyleYear> ModelStyleYears { get; set; } = [];
}
