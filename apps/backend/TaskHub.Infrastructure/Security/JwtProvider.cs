namespace TaskHub.Infrastructure.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TaskHub.Application.Abstractions;
using TaskHub.Application.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        this.options = options.Value;
    }

    public string GenerateToken(Guid userId, string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(options.Key));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.ExpireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}