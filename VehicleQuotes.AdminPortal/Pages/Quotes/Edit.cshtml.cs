using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.WebApi;
using VehicleQuotes.WebApi.Models;

namespace VehicleQuotes.AdminPortal.Pages.Quotes;

public class EditModel : PageModel
{
    private readonly VehicleQuotesContext _context;
    private readonly string _imagesPath;

    public EditModel(VehicleQuotesContext context, IConfiguration config)
    {
        _context = context;
        _imagesPath = config["QuoteImagesPath"] ??
            throw new InvalidOperationException("Config setting 'QuoteImagesPath' not found.");
    }

    [BindProperty]
    public IEnumerable<IFormFile>? ImageFiles { get; set; }

    public IActionResult OnGet(int id) => Page();

    public async Task<IActionResult> OnPostSaveAsync(int id)
    {
        var quote = await FindQuote(id);

        if (quote == null)
        {
            return NotFound();
        }

        if (ImageFiles is not null)
        {
            foreach (var imageFile in ImageFiles)
            {
                var imageFileName = await SaveImageFile(imageFile);
                quote.QuoteImages.Add(new() { FileName = imageFileName });
            }
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("./Edit", new { Id = id });
    }

    private async Task<Quote?> FindQuote(int id) =>
        await _context.Quotes
            .FirstOrDefaultAsync(m => m.ID == id);

    private async Task<string> SaveImageFile(IFormFile fileToSave)
    {
        var extension = Path.GetExtension(fileToSave.FileName).ToLowerInvariant();
        var fileName = $"{Path.GetRandomFileName()}{extension}";
        var filePath = Path.Combine(_imagesPath, fileName);

        using var stream = System.IO.File.Create(filePath);
        await fileToSave.CopyToAsync(stream);

        return fileName;
    }
}
