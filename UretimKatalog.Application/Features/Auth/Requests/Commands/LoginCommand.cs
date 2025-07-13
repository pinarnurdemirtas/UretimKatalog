using MediatR;
using UretimKatalog.Application.Features.Auth.Result;

namespace UretimKatalog.Application.Features.Auth.Requests.Commands
{
public record LoginCommand(string Email, string Password) : IRequest<LoginResult>;

}      
