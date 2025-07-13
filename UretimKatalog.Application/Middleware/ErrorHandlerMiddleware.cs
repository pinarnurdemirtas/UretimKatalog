// Api/Middleware/ErrorHandlerMiddleware.cs
using System.Net;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UretimKatalog.Application.Bases;   // BaseResponse burada

namespace UretimKatalog.Application.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next,
                                      ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                // ➊  İç içe sarımları ValidationException’e kadar aç
                var err = ex;
                while (err.InnerException != null &&
                       err is not ValidationException)
                {
                    err = err.InnerException;
                }

                var res  = ctx.Response;
                res.ContentType = "application/json";

                var body = new BaseResponse<object>
                {
                    Success = false
                };

                switch (err)
                {
                    case ValidationException ve:
                        body.StatusCode = HttpStatusCode.UnprocessableEntity;   
                        body.Errors = ve.Errors
                                      .Select(e => $"Hata: {e.ErrorMessage}")
                                      .ToList();
                        break;

                    case UnauthorizedAccessException:
                        body.StatusCode = HttpStatusCode.Unauthorized;
                        body.Errors = new() { err.Message };
                        break;

                    case KeyNotFoundException:
                        body.StatusCode = HttpStatusCode.NotFound;
                        body.Errors = new() { err.Message };
                        break;

                    case DbUpdateException:
                        body.StatusCode = HttpStatusCode.BadRequest;
                        body.Errors = new() { err.Message };
                        break;

                    default:
                        body.StatusCode = HttpStatusCode.InternalServerError;
                        body.Errors = new() { "Internal server error" };
                        break;
                }

                res.StatusCode = (int)body.StatusCode;
                await res.WriteAsync(JsonSerializer.Serialize(body));

                _logger.LogError(err, "Unhandled exception");
            }
        }
    }
}
