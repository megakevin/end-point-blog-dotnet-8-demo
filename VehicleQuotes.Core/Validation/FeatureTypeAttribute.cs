using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Core.Models;

namespace VehicleQuotes.Core.Validation;

public class FeatureTypeAttribute : ValidationAttribute
{
    private string AllowedValuesAsString => string.Join(", ", QuoteRule.FeatureTypes.All);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) return ValidationResult.Success;

        var isValid = QuoteRule.FeatureTypes.All.Contains((string)value);

        if (!isValid) return new ValidationResult(GetErrorMessage((string)value));

        return ValidationResult.Success;
    }

    private string GetErrorMessage(string value) =>
        $"{value} is not a valid feature type. Allowed values are: {AllowedValuesAsString}.";
}
