using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Pygma.App.Middleware
{
    public sealed class ProblemDetailsExceptionMiddleware
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ProblemDetailsExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ProblemDetailsExceptionMiddleware(RequestDelegate next,
            IWebHostEnvironment env,
            ILogger<ProblemDetailsExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env,
            ILogger<ProblemDetailsExceptionMiddleware> logger)
        {
            var problemDetails = new ProblemDetails();

            if (exception is BadHttpRequestException badHttpRequestException)
            {
                problemDetails.Title = "The request is invalid";
                problemDetails.Status = badHttpRequestException.StatusCode;
                problemDetails.Instance = $"urn:pygma:bad-request:{Guid.NewGuid()}";

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
                problemDetails.Instance = $"urn:pygma:internal-server-error:{Guid.NewGuid()}";

                if (env.IsDevelopment())
                {
                    problemDetails.Detail = exception.Demystify().ToString();
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            var result = JsonSerializer.Serialize(problemDetails);
            logger.LogCritical(exception, result);
            context.Response.ContentType = "application/json";
            
            return context.Response.WriteAsync(result);
        }
    }
}