using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Categories.Requests.Commands;
using UretimKatalog.Application.Features.Categories.Result;

namespace UretimKatalog.Api.Endpoints
{
    public static class CategoryEndpoints
    {
        public static RouteGroupBuilder MapCategories(this WebApplication app)
        {
            var group = app
                .MapGroup("/api/categories")
                .WithTags("Categories")
                .RequireAuthorization();

            group.MapPost("/", async (CreateCategoryDto dto, IMediator mediator) =>
            {
                var result = await mediator.Send(new CreateCategoryCommand(dto));
                return Results.Created($"/api/categories/{result.Id}", result);
            });


            group.MapPut("/", async (UpdateCategoryDto dto, IMediator mediator) =>
            {
                await mediator.Send(new UpdateCategoryCommand(dto));
                return Results.NoContent();
            });

            group.MapDelete("/{id:int}", async (int id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteCategoryCommand(id));
                return Results.NoContent();
            });

            return group;
        }
    }
}
