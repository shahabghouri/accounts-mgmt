using Accounts.Common.Exceptions;
using Accounts.Common.HelperDTO;
using Newtonsoft.Json;
using System.Net;


namespace Accounts.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UserTypeException ex)
            {
                ExceptionWrapperDTO exception = new ExceptionWrapperDTO();
                exception.Errors = new List<string> { ex.Message };
                exception.Message = "Operation cannot be completed due to following reason";
                exception.StatusCode = 499;
                exception.StatusDescription = "User Thrown Exception";
                await HandleExceptionAsync(httpContext, exception);
            }
            catch (Exception ex)
            {
                ExceptionWrapperDTO exception = new ExceptionWrapperDTO();
                exception.Errors = new List<string> { ex.Message };
                exception.Message = "Something went wrong!";
                exception.StatusCode = (int)HttpStatusCode.InternalServerError;
                exception.StatusDescription = "Internal Server Error";
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, ExceptionWrapperDTO exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
        }
    }

}
