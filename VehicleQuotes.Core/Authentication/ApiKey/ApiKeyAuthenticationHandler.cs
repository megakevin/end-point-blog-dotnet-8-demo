using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VehicleQuotes.Core.Data;

namespace VehicleQuotes.Core.Authentication.ApiKey;

class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private const string API_KEY_HEADER = "Api-Key";

    private readonly VehicleQuotesContext _context;

    public ApiKeyAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        VehicleQuotesContext context
    ) : base(options, logger, encoder)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Microsoft.Extensions.Primitives.StringValues apiKeyToValidate;

        if (!Request.Headers.TryGetValue(API_KEY_HEADER, out apiKeyToValidate))
        {
            return AuthenticateResult.Fail("Header Not Found.");
        }

        var apiKey = await _context.UserApiKeys
            .Include(uak => uak.User)
            .SingleOrDefaultAsync(uak => uak.Value == apiKeyToValidate.ToString());

        if (apiKey == null)
        {
            return AuthenticateResult.Fail("Invalid key.");
        }

        return AuthenticateResult.Success(CreateTicket(apiKey.User));
    }

    private AuthenticationTicket CreateTicket(IdentityUser user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return ticket;
    }
}