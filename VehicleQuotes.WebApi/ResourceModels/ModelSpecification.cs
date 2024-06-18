using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.WebApi.ResourceModels;

public class ModelSpecification
{
    public int ID { get; set; }
    [Required]
    public required string Name { get; set; }

    [Required]
    public ModelSpecificationStyle[] Styles { get; set; } = [];
}