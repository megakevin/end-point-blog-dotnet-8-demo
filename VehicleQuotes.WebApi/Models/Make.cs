using Microsoft.EntityFrameworkCore;
using VehicleQuotes.WebApi.Validation;

namespace VehicleQuotes.WebApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    [UniqueMakeName]
    public class Make
    {
        public int ID { get; set; }
        public required string Name { get; set; }
    }
}