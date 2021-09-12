using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Models.Errors;
using Services.Abstract;
using System.Net;

namespace API.Extensions
{
    public static class ExceptionMiddlwareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerService logger)
        {
            app.UseExceptionHandler(AppError =>
                {
                    AppError.Run(async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";


                            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                            if(contextFeature != null)
                            {
                                logger.LogError(contextFeature.Error.ToString());

                                await context.Response.WriteAsync(new ErrorDetails
                                {
                                    StatusCode = context.Response.StatusCode,
                                    Message = "Internal Server Error"
                                }.ToString());
                            }
                        }

                    );
                }
            );
        }
    }
}
