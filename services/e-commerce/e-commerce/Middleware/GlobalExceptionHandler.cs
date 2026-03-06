using System.Net;
using System.Text.Json;
using e_commerce.Domain.Common;

namespace e_commerce.Api.Middleware;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, "Domain rule violation: {Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.Message,
                type = "DomainException"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "An unexpected error occurred.",
                type = "InternalError"
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
