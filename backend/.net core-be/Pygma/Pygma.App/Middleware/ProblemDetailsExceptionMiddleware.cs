using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Pygma.App.Middleware
{
    public sealed class ProblemDetailsExceptionMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly RequestDelegate _next;

        public ProblemDetailsExceptionMiddleware(RequestDelegate next, IHostEnvironment env)
        {
            this._next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
        {
            var problemDetails = new ProblemDetails();

            if (exception is BadHttpRequestException badHttpRequestException)
            {
                problemDetails.Title = "The request is invalid";
                problemDetails.Status = badHttpRequestException.StatusCode;
                problemDetails.Instance = $"urn:CHANGEME:bad-request:{Guid.NewGuid()}";
                
                if (env.IsDevelopment())
                {
                    problemDetails.Detail = badHttpRequestException.Message;
                }
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                problemDetails.Title = "An unexpected error occurred!";
                problemDetails.Status = StatusCodes.Status500InternalServerError;
                problemDetails.Instance = $"urn:CHANGEME:internal-server-error:{Guid.NewGuid()}";

                //if (env.IsDevelopment())
                //{
                problemDetails.Detail = exception.Demystify().ToString();
                // }
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            // TODO: Log the exception          

            var result = JsonConvert.SerializeObject(problemDetails);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
