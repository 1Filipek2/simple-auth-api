using Microsoft.AspNetCore.Http;
using SimpleAuthApi.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UserAlreadyExistsException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { status = context.Response.StatusCode, message = ex.Message });
            await context.Response.WriteAsync(response);
        }
        catch (Exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var response = JsonSerializer.Serialize(new { status = context.Response.StatusCode, message = "Something went wrong" });
            await context.Response.WriteAsync(response);
        }
    }
}
