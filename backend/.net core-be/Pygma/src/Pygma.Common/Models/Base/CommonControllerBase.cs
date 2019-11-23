using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Pygma.Common.Models.Base
{
    [ApiController]
    //[Produces("application/json")]
    public abstract class CommonControllerBase : ControllerBase
    {
        protected static ActionResult InternalServerError(Exception exception = null,
            string message = null, object errorCode = null)
        {
            var problemDetails = new ProblemDetails
            {
                Type = $"urn:backbone:internal-server-error:{errorCode}",
                Title = message ?? "An unexpected error occurred!",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception?.Message,
                Instance = $"urn:backbone:internal-server-error:{Guid.NewGuid()}"
            };

            return new ObjectResult(problemDetails);
        }
    }
}
