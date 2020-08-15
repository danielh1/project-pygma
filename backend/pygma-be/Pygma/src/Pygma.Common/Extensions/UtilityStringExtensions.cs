using System;
using System.Linq.Expressions;

namespace Pygma.Common.Extensions
{
    public static class UtilityStringExtensions
    {
        public static Expression<Func<T, object>> CreateExpression<T>(this string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type);
            var access = Expression.Property(parameter, property);
            var convert = Expression.Convert(access, typeof(object));
            var function = Expression.Lambda<Func<T, object>>(convert, parameter);

            return function;
        }
    }
}
