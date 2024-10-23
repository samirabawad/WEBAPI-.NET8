using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace APIDOCKER.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Continua el pipeline si no hay excepciones
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex); // Maneja las excepciones
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        // Imprime detalles de la excepción en la consola para depuración
        Console.WriteLine($"Error: {exception.Message}");
        Console.WriteLine(exception.StackTrace);

        // Maneja excepciones específicas
        if (exception is KeyNotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            var errorResponse = new
            {
                message = exception.Message // Mensaje de la excepción
            };
            return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
        }

        // Maneja cualquier otra excepción
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        // Incluir detalles del error en la respuesta
        var generalErrorResponse = new
        {
            message = "Internal Server Error",
            details = exception.Message // Puedes incluir detalles adicionales aquí
        };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(generalErrorResponse));

    }

}
