using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pygma.Common.Models.ErrorHandling;
using Log = Serilog.Log;

namespace Pygma.App.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.ModelState.IsValid)
            {
                return;
            }

            var modelStateEntries = filterContext.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var errors = new List<ValidationError>();

            var details = "See validation errors for details";

            if (modelStateEntries.Any())
            {
                if (modelStateEntries.Length == 1 &&
                    modelStateEntries[0].Value.Errors.Count == 1 &&
                    string.IsNullOrEmpty(modelStateEntries[0].Key))
                {
                    details = modelStateEntries[0].Value.Errors[0].ErrorMessage;
                }
                else
                {
                    foreach (var (key, value) in modelStateEntries)
                    {
                        foreach (var modelStateError in value.Errors)
                        {
                            errors.Add(new ValidationError(key, modelStateError.ErrorMessage));
                        }
                    }
                }
            }
            
            var problemDetails = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Request Validation Error",
                Instance = $"urn:pygma:bad-request:{Guid.NewGuid()}",
                Detail = details,
                Extensions = {
                    new KeyValuePair<string, object>("validationErrors", errors)
                }
            };

            Log.Warning(JsonSerializer.Serialize(problemDetails));
            
            filterContext.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}