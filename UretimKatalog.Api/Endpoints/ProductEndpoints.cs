using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using UretimKatalog.Api.Models;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Products.Commands;


namespace UretimKatalog.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProducts(this WebApplication app)
        {
            var group = app
                .MapGroup("/api/products")
                .RequireAuthorization()
                .WithTags("Products");

            // POST: /api/products
            group.MapPost("/", async (CreateProductDto dto, IMediator mediator) =>
            {
                var created = await mediator.Send(new CreateProductCommand(dto));
                return Results.Created($"/api/products/{created.Id}",
                                       ApiResponse<ProductDto>.Ok(created));
            });

            // PUT: /api/products
            group.MapPut("/", async (UpdateProductDto dto, IMediator mediator) =>
            {
                await mediator.Send(new UpdateProductCommand(dto));
                return Results.Ok(ApiResponse<string>.Ok("Güncellendi"));
            });

            // DELETE: /api/products/{id}
            group.MapDelete("/{id:int}", async (int id, IMediator mediator) =>
            {
                await mediator.Send(new DeleteProductCommand(id));
                return Results.Ok(ApiResponse<string>.Ok("Silindi"));
            });

            return group;
        }
    }
}