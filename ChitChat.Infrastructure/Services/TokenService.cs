
using ChitChat.Application.Abstractions.Persistence;
using ChitChat.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ChitChat.Infrastructure.Services;

public sealed class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest request)
    {
        SymmetricSecurityKey symmetricSecurityKey = 
            new(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]!));

        var dateTimeNow = DateTime.UtcNow;

        JwtSecurityToken token = new(
            issuer: _configuration["AppSettings:ValidIssuer"],
            audience: _configuration["AppSettings:ValidAudience"],
            claims:
            [
                new Claim("email", request.Email),
            ],
            notBefore: dateTimeNow,
            expires: dateTimeNow.Add(TimeSpan.FromMinutes(500)),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
        );

        return Task.FromResult(new GenerateTokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            TokenExpireDate = dateTimeNow.Add(TimeSpan.FromMinutes(500))
        });
    }
}
