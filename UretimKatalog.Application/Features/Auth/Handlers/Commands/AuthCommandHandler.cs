using UretimKatalog.Application.Bases;
using UretimKatalog.Application.Contracts.Identity; 
using UretimKatalog.Application.Features.Auth.Requests.Commands;
using UretimKatalog.Application.Features.Auth.Result;
using MediatR;

namespace UretimKatalog.Application.Features.Auth.Handlers.Commands
{
    public class AuthCommandHandler : IRequestHandler<LoginCommand, LoginResult>,
                                      IRequestHandler<RegisterCommand, RegisterResult>
    {
        private readonly IAuthService _authService;

        public AuthCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.LoginAsync(request);
        }

        public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.RegisterAsync(request);
        }   
    }
}