using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;

namespace VehicleQuotes.WebApi.Validation
{
    public class ContainsYearsAttribute : ValidationAttribute
    {
        private string? propertyName;

        public ContainsYearsAttribute([CallerMemberName] string? propertyName = null)
        {
            this.propertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var isValid = ((string[])value).All(IsValidYear);

                if (!isValid)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        private bool IsValidYear(string value) =>
            !string.IsNullOrEmpty(value) && value.Length == 4 && value.All(char.IsDigit);

        private string GetErrorMessage() =>
            $"The {propertyName} field must be an array of strings containing four numbers.";
    }
}
