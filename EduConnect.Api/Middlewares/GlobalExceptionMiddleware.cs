using EduConnect.Api.Exceptions;
using System.Net;
using System.Text.Json;

namespace EduConnect.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next )
        {
            _next = next;
         
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetStatusCode(error);

            var resultMessage = JsonSerializer.Serialize(new
            {
                Success = false,
                Message = error.Message,
                Detail = GetErrorDetail(error)
            });

            await context.Response.WriteAsync(resultMessage);
        }

        private int GetStatusCode(Exception error)
        {
            return error switch
            {
                ClientSideException => (int)HttpStatusCode.BadRequest,
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError,
            };
        }

        private string GetErrorDetail(Exception error)
        {
            return error is NotFoundException ? "Requested resource not found." : error.StackTrace;
        }
    }
}
