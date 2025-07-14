using UretimKatalog.Application.Bases;
using UretimKatalog.Application.Contracts.Identity;
using UretimKatalog.Application.Features.Auth.Requests.Commands;
using UretimKatalog.Application.Features.Auth.Result;

namespace UretimKatalog.Application.Contracts.Identity
{
public interface IAuthService
{
    Task<RegisterResult> RegisterAsync(RegisterCommand command);
    Task<LoginResult>    LoginAsync   (LoginCommand command);
}


}