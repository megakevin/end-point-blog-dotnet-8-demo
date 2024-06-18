using VehicleQuotes.Core.Models;
using VehicleQuotes.Core.Repositories;

namespace VehicleQuotes.Core.Validation;

public class UniqueMakeNameAttribute : BaseUniqueAttribute<Make, IMakeRepository>
{
    protected override Make? FindRecord(Make input, IMakeRepository repo) =>
        repo.FindByName(input.Name);

    protected override bool IsSameRecord(Make input, Make existing) =>
        input.ID == existing.ID;

    protected override string GetErrorMessage(Make input) =>
        $"The Make name '{input.Name}' is already in use.";
}
