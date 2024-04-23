using VehicleQuotes.RazorTemplates.Services;
using VehicleQuotes.WebApi.ResourceModels;
using VehicleQuotes.RazorTemplates.ViewModels;

namespace VehicleQuotes.WebApi.Services;

public class QuoteGeneratedMailer
{
    private readonly IMailer _mailer;
    private readonly IRazorViewRenderer _razorViewRenderer;
    private readonly string _emailTo;
    private readonly string _emailToName;

    public QuoteGeneratedMailer(IMailer mailer, IRazorViewRenderer razorViewRenderer, IConfiguration config)
    {
        _mailer = mailer;
        _razorViewRenderer = razorViewRenderer;
        _emailTo = config["QuoteGeneratedEmailRecipientEmail"]!;
        _emailToName = config["QuoteGeneratedEmailRecipientName"]!;
    }

    public async Task SendAsync(QuoteGeneratedViewModel payload)
    {
        string body = await _razorViewRenderer.Render(
            "/Views/Emails/QuoteGenerated.cshtml", payload
        );

        await _mailer.SendMailAsync(new() {
            To = _emailTo,
            ToName = _emailToName,
            Subject = $"VehicleQuotes - New Quote Generated - Quote #{payload.ID}",
            Body = body
        });
    }
}
