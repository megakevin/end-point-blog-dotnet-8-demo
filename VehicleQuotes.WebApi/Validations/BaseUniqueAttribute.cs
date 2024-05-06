using System.ComponentModel.DataAnnotations;

namespace VehicleQuotes.WebApi.Validation;

[AttributeUsage(AttributeTargets.Class)]
public abstract class BaseUniqueAttribute<TEntity, TRepo> : ValidationAttribute
{
    protected abstract TEntity? FindRecord(TEntity input, TRepo repository);
    protected abstract bool IsSameRecord(TEntity input, TEntity existing);
    protected abstract string GetErrorMessage(TEntity input);

    protected override ValidationResult? IsValid(object? input, ValidationContext context)
    {
        if (input == null) return ValidationResult.Success;

        var repo = context.GetService<TRepo>();
        if (repo == null) return ValidationResult.Success;

        return Validate((TEntity)input, repo);
    }

    private ValidationResult? Validate(TEntity input, TRepo repo)
    {
        var existing = FindRecord(input, repo);

        if (existing != null && !IsSameRecord(input, existing))
        {
            return new ValidationResult(GetErrorMessage(input));
        }

        return ValidationResult.Success;
    }
}
