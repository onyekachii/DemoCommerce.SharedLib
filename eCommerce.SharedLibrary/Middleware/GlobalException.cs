
using eCommerce.SharedLibrary.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace eCommerce.SharedLibrary.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string msg = "Server error";
            int statusCode = (int) HttpStatusCode.InternalServerError;
            string title = "error";
            try
            {
                await next(context);
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests)
                {
                    title = "warning";
                    msg = "Too many requests";
                    statusCode = (int)StatusCodes.Status429TooManyRequests;
                }
                if(context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "alert";
                    msg = "Unauthorized";
                    statusCode = (int)StatusCodes.Status401Unauthorized;
                }
                if(context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "out of access";
                    msg = "Not allowed";
                    statusCode = (int)StatusCodes.Status403Forbidden;
                }
            }catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                if(ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "timeout";
                    msg = "Request Timeout";
                    statusCode = StatusCodes.Status408RequestTimeout;
                }
            }
            finally
            {
                await ModifyHeader(context, title, msg, statusCode);
            }
        }

        private async Task ModifyHeader(HttpContext context, string title, string msg,  int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails()
            {
                Detail = msg,
                Status = statusCode,
                Title = title
            }), CancellationToken.None);
            return;
        }
    }
}
