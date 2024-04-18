using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VehicleQuotes.WebApi.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Size
    {
        public int ID { get; set; }
        public required string Name { get; set; }
    }
}