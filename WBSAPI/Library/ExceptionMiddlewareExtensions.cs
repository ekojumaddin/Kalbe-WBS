using KN2021_GlobalClient_NetCore;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using WBSBE.Common.Library.Interface;
using WBSBE.Common.Library.Response;

namespace WBSBE.Library
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");

                #region inject error into global api
                clsGlobalAPI result = new clsGlobalAPI();
                result.bitSuccess = false;
                result.bitError = true;
                result.txtStackTrace = ex.StackTrace;
                result.txtErrorMessage = ex.Message;
                #endregion

                await HandleExceptionAsync(httpContext, result);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, clsGlobalAPI result)
        {
            context.Response.ContentType = "application/json";
            if (result.txtErrorMessage.ToLower().Contains("unauthorized operation") || result.txtErrorMessage.ToLower().Contains("re login"))
            {
                result.txtStackTrace = string.Empty;
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
            await context.Response.WriteAsync(Newtonsoft.Json.JsonConvert.SerializeObject(result));
        }
    }
}
