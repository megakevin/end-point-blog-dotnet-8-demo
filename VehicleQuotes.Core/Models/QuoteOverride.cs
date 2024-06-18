using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.Core.Models;

[Index(nameof(ModelStyleYearID), IsUnique = true)]
public class QuoteOverride
{
    public int ID { get; set; }
    public int ModelStyleYearID { get; set; }
    public int Price { get; set; }

    public ModelStyleYear ModelStyleYear { get; set; } = default!;
}
