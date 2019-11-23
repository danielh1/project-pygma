using Microsoft.AspNetCore.Mvc;
using Pygma.App.Filters;

namespace Pygma.App.Extensions
{
    public static class MvcFiltersExtensions
    {
        public static void AddCoreFilters(this MvcOptions options)
        {
            options.Filters.Add(new UserFilterAttribute());
            options.Filters.Add(new ValidateModelStateFilterAttribute());
        }
    }
}
