using ChitChat.Application.Exceptions;
using ChitChat.Domain.Entities;

namespace ChitChat.API.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                EmailOrUsernameInUseException => StatusCodes.Status409Conflict,
                InvalidCredentialsException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            await context.Response.WriteAsync(new Result()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
            }.ToString());
        }
    }
}
