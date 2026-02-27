using System.Net;
using Thebook.Exceptions;

namespace Thebook.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no controlada");
                await HandleExceptionAsync(context, ex);

            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;
            
            switch (exception)
            {
                case BusinessException:
                    status = HttpStatusCode.BadRequest;
                    break;
                
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    message = "Ocurrió un error interno en el servidor.";
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;


            await context.Response.WriteAsJsonAsync(new
            {
                error = message
            });
        }
    }
}
