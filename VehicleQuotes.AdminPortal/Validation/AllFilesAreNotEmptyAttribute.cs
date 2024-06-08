using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.AdminPortal.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class AllFilesAreNotEmptyAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext context)
    {
        if (value == null) return ValidationResult.Success;

        ThrowIfTypeIsNotSupported(value);

        if (!CheckIfIsValid(value)) return new ValidationResult(GetErrorMessage());

        return ValidationResult.Success;
    }

    private void ThrowIfTypeIsNotSupported(object value)
    {
        if (value is not IEnumerable<IFormFile>)
        {
            throw new ArgumentException($"{GetType().Name} only works with properties of type IEnumerable<IFormFile>.");
        }
    }

    private static bool CheckIfIsValid(object value)
    {
        var files = (IEnumerable<IFormFile>)value;
        return files.All(file => file.Length > 0);
    }

    private static string GetErrorMessage() => "Some of the selected files appear to be empty.";
}
