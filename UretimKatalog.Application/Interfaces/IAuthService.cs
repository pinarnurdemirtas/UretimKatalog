using UretimKatalog.Application.DTOs;

public interface IAuthService
{
    Task<TokenResponseDto> AuthenticateAsync(LoginDto login);
}