using MediatR;
using Microsoft.AspNetCore.Builder;
using UretimKatalog.Application.Features.Auth.Requests.Commands;
using UretimKatalog.Application.Features.Auth.Result;
using UretimKatalog.Api.Models; 

namespace UretimKatalog.Api.Endpoints
{
    public static class AuthEndpoints
    {
        public static RouteGroupBuilder MapAuth(this WebApplication app)
        {
            var group = app
            .MapGroup("/api/auth")
            .WithTags("Auth");

            // POST /api/auth/login
            group.MapPost("/login", async (LoginCommand cmd, IMediator mediator) =>
            {
                var result = await mediator.Send(cmd);               
                return Results.Ok(ApiResponse<LoginResult>.Ok(result));
            });

            // POST /api/auth/register
            group.MapPost("/register", async (RegisterCommand cmd, IMediator mediator) =>
            {
                var result = await mediator.Send(cmd);               
                return Results.Created($"/api/users/{result.Id}",
                                        ApiResponse<RegisterResult>.Ok(result));
            });

            return group;
        }
    }
}
