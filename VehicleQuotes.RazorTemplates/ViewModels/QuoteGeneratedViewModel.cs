namespace VehicleQuotes.RazorTemplates.ViewModels;

public class QuoteGeneratedViewModel
{
    public required int ID { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required int OfferedQuote { get; set; }
    public required string Message { get; set; }
    public required string Year { get; set; }
    public required string Make { get; set; }
    public required string Model { get; set; }
}