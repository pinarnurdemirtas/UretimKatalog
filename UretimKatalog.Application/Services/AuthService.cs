using System;
using System.Text;
using System.Security.Claims;                 
using Microsoft.Extensions.Configuration;       
using Microsoft.IdentityModel.Tokens;          
using System.IdentityModel.Tokens.Jwt;         
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;


namespace UretimKatalog.Application.Services
{
    public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    public AuthService(IConfiguration config) => _config = config;

    public Task<TokenResponseDto> AuthenticateAsync(LoginDto login)
    {
        if (login.UserName != "admin" || login.Password != "password")
            throw new UnauthorizedAccessException("Geçersiz kullanıcı veya parola");

        var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpireMinutes"]));
        var claims  = new[] { new Claim(JwtRegisteredClaimNames.Sub, login.UserName) };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: expires,
            signingCredentials: creds
        );

        return Task.FromResult(new TokenResponseDto
        {
            Token     = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expires
        });
    }
}

}
