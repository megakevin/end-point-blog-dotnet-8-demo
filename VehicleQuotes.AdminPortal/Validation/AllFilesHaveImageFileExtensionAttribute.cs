using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.AdminPortal.Validation;

[AttributeUsage(AttributeTargets.Property)]
public class AllFilesHaveImageFileExtensionAttribute : ValidationAttribute
{
    private static readonly string[] imageExtensions = [".png", ".jpg", ".jpeg", ".gif", ".bmp"];

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
        return files.All(HasImageExtension);
    }

    private static bool HasImageExtension(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension) || !imageExtensions.Contains(extension))
        {
            return false;
        }

        return true;
    }

    private static string GetErrorMessage()
    {
        var allowedExtensions = string.Join(", ", imageExtensions);
        return $"Only the following file extensions are allowed: {allowedExtensions}.";
    }
}
