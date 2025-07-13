using MediatR;
using UretimKatalog.Application.Features.Auth.Result;

namespace UretimKatalog.Application.Features.Auth.Requests.Commands
{
public record RegisterCommand(string Email, string Password, string FullName)
       : IRequest<RegisterResult>; 
}