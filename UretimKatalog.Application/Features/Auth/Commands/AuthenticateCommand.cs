using System.Threading;
using System.Threading.Tasks;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Interfaces;
using MediatR;

namespace UretimKatalog.Application.Features.Auth.Commands
{

    public record AuthenticateCommand(string Username, string Password) : IRequest<TokenResponseDto>;

    public class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, TokenResponseDto>
    {
        private readonly IAuthService _authService;

        public AuthenticateCommandHandler(IAuthService authService)
            => _authService = authService;

        public async Task<TokenResponseDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            var loginDto = new LoginDto { Username = request.Username, Password = request.Password };
            return await _authService.AuthenticateAsync(loginDto);
        }
    }
}
