// Api/Endpoints/OrderEndpoints.cs
using MediatR;
using Microsoft.AspNetCore.Builder;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Orders.Requests.Commands;
using UretimKatalog.Application.Features.Orders.Responses;

namespace UretimKatalog.Api.Endpoints
{
    public static class OrderEndpoints
    {
        public static RouteGroupBuilder MapOrders(this WebApplication app)
        {
            var g = app.MapGroup("/api/orders")
                        .RequireAuthorization()
                        .WithTags("Orders");


            
            g.MapPost("/", async (CreateOrderCommand cmd, IMediator m) =>
            {
                CreateOrderResult res = await m.Send(cmd);
                return Results.Created($"/api/orders/{res.Id}", res);
            });


            return g;
        }
    }
}
