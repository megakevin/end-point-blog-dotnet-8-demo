using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.WebApi.ResourceModels;

public class QuoteOverrideSpecification
{
    public int ID { get; set; }

    [Required]
    public required string Year { get; set; }

    [Required]
    public required string Make { get; set; }

    [Required]
    public required string Model { get; set; }

    [Required]
    public required string BodyType { get; set; }

    [Required]
    public required string Size { get; set; }

    [Required]
    public int Price { get; set; }
}