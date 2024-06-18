namespace VehicleQuotes.Core.Services;

public class UserJwt
{
    public required string Token { get; set; }
    public DateTime Expiration { get; set; }
}