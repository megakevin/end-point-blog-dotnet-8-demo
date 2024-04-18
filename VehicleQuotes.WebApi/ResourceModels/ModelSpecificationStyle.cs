using System.ComponentModel.DataAnnotations;
using VehicleQuotes.WebApi.Validation;

namespace VehicleQuotes.WebApi.ResourceModels
{
    public class ModelSpecificationStyle
    {
        [Required]
        [VehicleBodyType]
        public required string BodyType { get; set; }
        [Required]
        [VehicleSize]
        public required string Size { get; set; }

        [Required]
        [MinLength(1)]
        [ContainsYears]
        public string[] Years { get; set; } = [];
    }
}