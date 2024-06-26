using VehicleQuotes.Core.Data;
using VehicleQuotes.Core.Models;

namespace VehicleQuotes.Core.Repositories;

public interface IMakeRepository
{
    Make? FindByName(string name);
}

public class MakeRepository : IMakeRepository
{
    private readonly VehicleQuotesContext _context;

    public MakeRepository(VehicleQuotesContext context)
    {
        _context = context;
    }

    public Make? FindByName(string name) =>
        _context.Makes.FirstOrDefault(m => m.Name == name);
}
