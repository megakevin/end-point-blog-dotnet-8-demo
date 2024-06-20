using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace VehicleQuotes.Core.Services;

public class JwtService
{
    private const int EXPIRATION_MINUTES = 1;

    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public UserJwt CreateToken(IdentityUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(
            CreateTokenDescriptor(user, expiration)
        );

        return new UserJwt
        {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration
        };
    }

    private SecurityTokenDescriptor CreateTokenDescriptor(IdentityUser user, DateTime expiration) =>
        new()
        {
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            Subject = new ClaimsIdentity(CreateClaims(user)),
            Expires = expiration,
            SigningCredentials = CreateSigningCredentials()
        };

    private Claim[] CreateClaims(IdentityUser user) =>
        [
            new Claim(
                JwtRegisteredClaimNames.Sub,
                _configuration["Jwt:Subject"] ??
                    throw new InvalidOperationException("Config setting 'Jwt:Subject' not found.")
            ),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!)
        ];

    private SigningCredentials CreateSigningCredentials() =>
        new (
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"] ??
                        throw new InvalidOperationException("Config setting 'Jwt:Subject' not found.")
                )
            ),
            SecurityAlgorithms.HmacSha256
        );
}
