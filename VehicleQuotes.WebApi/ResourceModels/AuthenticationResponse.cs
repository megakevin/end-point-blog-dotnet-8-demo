using System;

namespace VehicleQuotes.WebApi.ResourceModels
{
    public class AuthenticationResponse
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}