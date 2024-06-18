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

        var token = CreateJwtToken(
            CreateClaims(user),
            CreateSigningCredentials(),
            expiration
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        return new UserJwt {
            Token = tokenHandler.WriteToken(token),
            Expiration = expiration
        };
    }

    private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration) =>
        new (
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private Claim[] CreateClaims(IdentityUser user) =>
        [
            new Claim(
                JwtRegisteredClaimNames.Sub,
                _configuration["Jwt:Subject"] ??
                    throw new InvalidOperationException("Config setting 'Jwt:Subject' not found.")
            ),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
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