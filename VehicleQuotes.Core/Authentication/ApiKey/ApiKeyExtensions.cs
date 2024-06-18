using Microsoft.AspNetCore.Authentication;
using VehicleQuotes.Core.Authentication.ApiKey;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApiKeyExtensions
{
    public static AuthenticationBuilder AddApiKey(this AuthenticationBuilder builder)
    {
        return builder.AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>(
            ApiKeyDefaults.AuthenticationScheme,
            options => { }
        );
    }
}