using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Pygma.Common.Filters;
using Pygma.Services.Users;

namespace Pygma.App.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var usersService =
                (IUsersService) filterContext.HttpContext.RequestServices.GetService(typeof(IUsersService));

            var actionDescriptor =
                (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor) filterContext.ActionDescriptor;

            if (actionDescriptor.ControllerTypeInfo.IsDefined(typeof(SkipInactiveUserFilter), false) ||
                actionDescriptor.MethodInfo.IsDefined(typeof(SkipInactiveUserFilter), false))
            {
                return;
            }
            
            var user = usersService.GetUser();

            if (user == null)
            {
                filterContext.Result = new ForbidResult();
            } else if (!user.Active)
            {
                filterContext.Result = new UnauthorizedResult();
            }
        }
    }
}