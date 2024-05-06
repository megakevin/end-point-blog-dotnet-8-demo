using System.ComponentModel.DataAnnotations;
using VehicleQuotes.WebApi.Models;
using VehicleQuotes.WebApi.Repositories;

namespace VehicleQuotes.WebApi.Validation;

public class UniqueMakeNameAttribute : BaseUniqueAttribute<Make, IMakeRepository>
{
    protected override Make? FindRecord(Make input, IMakeRepository repo) =>
        repo.FindByName(input.Name);

    protected override bool IsSameRecord(Make input, Make existing) =>
        input.ID == existing.ID;

    protected override string GetErrorMessage(Make input) =>
        $"The Make name '{input.Name}' is already in use.";
}
