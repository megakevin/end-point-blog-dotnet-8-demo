using System.ComponentModel.DataAnnotations;
using VehicleQuotes.WebApi.Models;
using VehicleQuotes.WebApi.Repositories;

namespace VehicleQuotes.WebApi.Validation;

[AttributeUsage(AttributeTargets.Class)]
public class UniqueMakeNameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value == null) return ValidationResult.Success;

        var repo = context.GetService<IMakeRepository>();
        if (repo == null) return ValidationResult.Success;

        return Validate((Make)value, repo);
    }

    private static ValidationResult? Validate(Make input, IMakeRepository repo)
    {
        var existing = repo.FindByName(input.Name);

        if (existing != null && input.ID != existing.ID)
        {
            return new ValidationResult($"The Make name '{input.Name}' is already in use.");
        }

        return ValidationResult.Success;
    }
}

