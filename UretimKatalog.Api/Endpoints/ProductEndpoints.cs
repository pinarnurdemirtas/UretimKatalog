using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Product.Requests.Commands;
using UretimKatalog.Application.Features.Product.Requests.Queries;
using UretimKatalog.Application.Features.Product.Result;

namespace UretimKatalog.Api.Endpoints
{
    public static class ProductEndpoints
    {
        public static RouteGroupBuilder MapProducts(this WebApplication app)
        {
            var g = app.MapGroup("/api/products")
                        .RequireAuthorization()
                        .WithTags("Products");


            g.MapPost("/", async (CreateProductCommand cmd, IMediator m) =>
            {
                CreateProductResult res = await m.Send(cmd);
                return Results.Created($"/api/products/{res.Id}", res);
            });

            g.MapPut("/", async (UpdateProductCommand cmd, IMediator m) =>
            {
                await m.Send(cmd);
                return Results.NoContent();
            });

            g.MapPut("/{id:int}/price", async (int id, decimal price, IMediator m) =>
            {
                await m.Send(new UpdateProductPriceCommand(id, price));
                return Results.NoContent();
            });

            g.MapPut("/{id:int}/stock", async (int id, int stock, IMediator m) =>
            {
                await m.Send(new UpdateProductStockCommand(id, stock));
                return Results.NoContent();
            });

            g.MapPut("/{id:int}/toggle", async (int id, IMediator m) =>
            {
                await m.Send(new ToggleProductStatusCommand(id));
                return Results.NoContent();
            });

            g.MapDelete("/{id:int}", async (int id, IMediator m) =>
            {
                await m.Send(new DeleteProductCommand(id));
                return Results.NoContent();
            });

            g.MapGet("/", async (IMediator m) =>
            {
                IEnumerable<ProductDto> list = await m.Send(new GetAllProductsQuery());
                return Results.Ok(list);
            });

            g.MapGet("/{id:int}", async (int id, IMediator m) =>
            {
                ProductDto dto = await m.Send(new GetProductByIdQuery(id));
                return Results.Ok(dto);
            });

            return g;
        }
    }
}
