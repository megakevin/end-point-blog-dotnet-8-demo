using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VehicleQuotes.AdminPortal.Validation;
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

    public Quote Quote { get; set; } = default!;

    [BindProperty]
    [AllFilesAreNotEmpty]
    [AllFilesHaveImageFileExtension]
    public IEnumerable<IFormFile>? ImageFiles { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var quote = await FindQuote(id);

        if (quote == null)
        {
            return NotFound();
        }

        Quote = quote;

        return Page();
    }

    public async Task<IActionResult> OnPostSaveAsync(int id)
    {
        var quote = await FindQuote(id);

        if (quote == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return Page();
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

    public async Task<IActionResult> OnPostDeleteImageAsync(int id, int imageId)
    {
        var quote = await FindQuote(id);

        if (quote == null)
        {
            return NotFound();
        }

        var imageToDelete = quote.GetImageById(imageId);

        if (imageToDelete == null)
        {
            return NotFound();
        }

        DeleteImageFile(imageToDelete.FileName);
        _context.QuoteImages.Remove(imageToDelete);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Edit", new { Id = id });
    }

    private async Task<Quote?> FindQuote(int id) =>
        await _context.Quotes
            .Include(m => m.QuoteImages)
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

    private void DeleteImageFile(string fileName)
    {
        var filePath = Path.Combine(_imagesPath, fileName);
        System.IO.File.Delete(filePath);
    }
}
