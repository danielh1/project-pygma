using System;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Pygma.Common.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        /// <summary>
        /// Returns the route value or the selected route value name. The generic of GUID will not work
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpContextAccessor"></param>
        /// <param name="routeValueName"></param>
        /// <returns></returns>
        public static T GetRouteValue<T>(this IHttpContextAccessor httpContextAccessor, string routeValueName)
        {
            var routeValue = httpContextAccessor.HttpContext.GetRouteValue(routeValueName);
            return (T) Convert.ChangeType(routeValue, typeof(T), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Returns the route value or the selected route value name. The generic of GUID will not work
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpContextAccessor"></param>
        /// <param name="routeValueName"></param>
        /// <returns></returns>
        public static T GetRouteValueIfExist<T>(this IHttpContextAccessor httpContextAccessor, string routeValueName)
        {
            var routeValue = httpContextAccessor.HttpContext.Request.Query.ContainsKey(routeValueName);

            return routeValue ? GetRouteValue<T>(httpContextAccessor, routeValueName) : default;
        }
    }
}