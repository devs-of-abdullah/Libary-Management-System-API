using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtService
{
    private readonly string _key;

    public JwtService(IConfiguration config)
    {
        string? envKey = Environment.GetEnvironmentVariable("JWT_SECRET");

        string? configKey = config["Jwt:Key"];

        _key = !string.IsNullOrWhiteSpace(envKey) ? envKey : !string.IsNullOrWhiteSpace(configKey) ? configKey : "";
    }

    public string GenerateToken(int staffId, string role)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, staffId.ToString()),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddDays(7),
            claims: claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

