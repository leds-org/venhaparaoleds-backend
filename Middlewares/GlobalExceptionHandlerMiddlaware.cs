using System.Net;
using System.Text.Json;
using TrilhaBackendLeds.Exceptions;

namespace TrilhaBackendLeds.Middlewares
{
    public class GlobalExceptionHandlerMiddlaware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddlaware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AggregateException aggEx)
            {
                foreach (var ex in aggEx.InnerExceptions)
                {
                    await HandleException(context, ex);
                }
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            switch (exception)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    break;

                case BadRequestException:
                    status = HttpStatusCode.BadRequest;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "Ocorreu um erro inesperado.";
                    Console.WriteLine($"Erro: {exception.Message}");
                    Console.WriteLine($"StackTrace: {exception.StackTrace}");
                    break;
            }

            var response = new
            {
                message,
                statusCode = (int)status,
                timestamp = DateTime.UtcNow
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
