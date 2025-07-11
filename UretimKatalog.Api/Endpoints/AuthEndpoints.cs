using MediatR;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Auth.Commands;  

namespace UretimKatalog.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuth(this WebApplication app)
        {
            var group = app.MapGroup("/api/auth");

            group.MapPost("/login", async (IMediator mediator, LoginDto dto) =>
            {
                //dto yerine dto.Username ve dto.Password gönderiyoruz
                var token = await mediator.Send(new AuthenticateCommand(dto.Username, dto.Password));
                return Results.Ok(ApiResponse<TokenResponseDto>.Ok(token));
            })
            .WithName("Login")
            .WithTags("Auth");

            return group;
        }
    }
}
