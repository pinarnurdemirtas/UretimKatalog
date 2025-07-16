using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UretimKatalog.Application.Contracts.Identity;
using UretimKatalog.Application.Features.Auth.Requests.Commands;
using UretimKatalog.Application.Features.Auth.Result;
using UretimKatalog.Domain.Interfaces;
using UretimKatalog.Domain.Models;
using AutoMapper;

namespace UretimKatalog.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;
        private readonly IConfiguration _cfg;
        private readonly IMapper _map;

        public AuthService(IUnitOfWork uow, IConfiguration cfg, IMapper map)
        {
            _uow = uow;
            _cfg = cfg;
            _map = map;
        }

        public async Task<RegisterResult> RegisterAsync(RegisterCommand req)
        {
            if (await _uow.Users.GetByEmailAsync(req.Email) is not null)
                throw new InvalidOperationException("Bu e-posta ile zaten kullanıcı var.");

            var user = _map.Map<User>(req);
            user.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);

            await _uow.Users.AddAsync(user);
            await _uow.CommitAsync();

            return _map.Map<RegisterResult>(user);
        }

        public async Task<LoginResult> LoginAsync(LoginCommand req)
        {
            var user = await _uow.Users.GetByEmailAsync(req.Email)
                       ?? throw new UnauthorizedAccessException("E-posta veya şifre hatalı.");

            if (!BCrypt.Net.BCrypt.Verify(req.Password, user.Password))
                throw new UnauthorizedAccessException("E-posta veya şifre hatalı.");

            var result = _map.Map<LoginResult>(user);
            result.Token = GenerateJwtToken(user);
            return result;
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("scope", "orderservice")
    };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
