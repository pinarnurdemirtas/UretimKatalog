using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using UretimKatalog.Application.DTOs;
using UretimKatalog.Application.Features.Review.Requests.Commands;
using UretimKatalog.Application.Features.Review.Results;
using UretimKatalog.Application.Features.Reviews.Requests.Commands;


namespace UretimKatalog.Api.Endpoints
{
    public static class ReviewEndpoints
    {
        public static RouteGroupBuilder MapReviews(this WebApplication app)
        {
            var g = app.MapGroup("/api/reviews")
                        .RequireAuthorization()
                        .WithTags("Reviews");

            g.MapPost("/", async (CreateReviewCommand cmd, IMediator m) =>
            {
                CreateReviewResult res = await m.Send(cmd);
                return Results.Created($"/api/reviews/{res.ReviewId}", res);
            });

            g.MapDelete("/{id:int}", async (int id, IMediator m) =>
            {
                await m.Send(new DeleteReviewCommand(id));
                return Results.NoContent();
            });


            return g;
        }
    }
}
