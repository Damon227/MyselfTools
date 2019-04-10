using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityDemo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace IdentityDemo.Middlewares
{
    public class InternalServerErrorExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InternalServerErrorExceptionHandlerMiddleware" /> class
        /// </summary>
        /// <param name="next"></param>
        public InternalServerErrorExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        ///     Process an individual request.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            ApiResponse response = new ApiResponse
            {
                Code = 0,
                ExtraData = new Dictionary<string, object>(),
                Message = string.Empty
            };

            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                if (httpContext.Response.HasStarted)
                {
                    throw;
                }

                if (httpContext.Request.Query.ContainsKey("throw"))
                {
                    throw;
                }


                response.Code = 500;
                response.Message = e.Message;


                //if (!hostingEnvironment.IsProduction() || httpContext.Request.Query.ContainsKey("debug")
                //                                       || httpContext.Request.Query.ContainsKey("Debug")
                //                                       || httpContext.Request.Query.ContainsKey("stacktrace")
                //                                       || httpContext.Request.Query.ContainsKey("stackTrace")
                //                                       || httpContext.Request.Query.ContainsKey("StackTrace"))
                //{
                //    response.StackTrace = e.StackTrace;
                //}

                httpContext.Response.Clear();
                httpContext.Response.Headers["Access-Control-Allow-Credentials"] = new StringValues("true");
                httpContext.Response.Headers["Access-Control-Allow-Headers"] = new StringValues("*");
                httpContext.Response.Headers["Access-Control-Allow-Methods"] = new StringValues("*");
                httpContext.Response.Headers["Access-Control-Allow-Origin"] = new StringValues("*");
                httpContext.Response.Headers["Access-Control-Expose-Headers"] = new StringValues("*");
                httpContext.Response.StatusCode = 200;
                httpContext.Response.ContentType = "application/json; charset=utf-8";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}
