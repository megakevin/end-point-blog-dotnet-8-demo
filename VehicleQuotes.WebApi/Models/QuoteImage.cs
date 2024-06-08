namespace VehicleQuotes.WebApi.Models;

public class QuoteImage
{
    public int ID { get; set; }
    public string FileName { get; set; } = default!;

    public int QuoteId { get; set; }
    public Quote Quote { get; set; } = default!;

    public string Url => $"~/uploads/{FileName}";
}
