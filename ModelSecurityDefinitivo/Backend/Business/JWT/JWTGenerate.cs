using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Entity.Model;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Entity.DTOs.Write;

namespace Business.JWT;

public class JWTGenerate
{
    private readonly IConfiguration _configuration;
    public JWTGenerate(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateJWT(LoginDTO user)
    {
        var Claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username)
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        int expirationMinutes = int.Parse(_configuration["Jwt:Expirate"]!);

        var JwtConfig = new JwtSecurityToken(
            claims: Claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(JwtConfig);
    } 

}
