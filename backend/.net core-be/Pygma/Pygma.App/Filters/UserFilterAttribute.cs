using Microsoft.AspNetCore.Mvc.Filters;

namespace Pygma.App.Filters
{
    public class UserFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
//            var usersService = (IUsersService)filterContext.HttpContext.RequestServices.GetService(typeof(IUsersService));
//
//            var actionDescriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor;
//
//            if (actionDescriptor.ControllerTypeInfo.IsDefined(typeof(SkipInactiveUserInterceptorAttribute), false) ||
//                actionDescriptor.MethodInfo.IsDefined(typeof(SkipInactiveUserInterceptorAttribute), false))
//            {
//                return;
//            }
//
//            var user = usersService.GetUser();
//
//            if (user == null || !user.Active)
//            {
//                filterContext.Result = new ForbidResult();
//            }
        }
    }
}