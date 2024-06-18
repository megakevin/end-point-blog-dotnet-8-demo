using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.Core.Data;
using VehicleQuotes.Core.Models;

namespace VehicleQuotes.AdminPortal.Pages.Quotes;

public class IndexModel : PageModel
{
    private readonly VehicleQuotesContext _context;

    public IndexModel(VehicleQuotesContext context)
    {
        _context = context;
    }

    public IList<Quote> Quotes { get;set; } = default!;

    public async Task OnGetAsync()
    {
        Quotes = await _context.Quotes
            .Include(q => q.BodyType)
            .Include(q => q.ModelStyleYear)
            .Include(q => q.Size).ToListAsync();
    }
}
