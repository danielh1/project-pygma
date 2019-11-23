using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pygma.Data.Abstractions.Search;
using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.SearchSpecifications
{
     public abstract class BaseSpecification<T> : ISpecification<T>, ISearchableSpecification<ISearchCriteria<T>>
    {
        protected BaseSpecification()
        {

        }

        public List<Expression<Func<T, bool>>> Criteria { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        private Expression<Func<T, object>> ApplyOrderBy(string propertyName)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type);
            var access = Expression.Property(parameter, property);
            var convert = Expression.Convert(access, typeof(object));
            var function = Expression.Lambda<Func<T, object>>(convert, parameter);

            return function;
        }

        public void ApplyPagingAndOrder(ISearchCriteria<T> criteria)
        {
            ApplyPaging(criteria.Skip, criteria.Take);

            if (criteria.OrderDirectionEnum == OrderDirectionEnum.Asc)
            {
                ApplyOrderBy(criteria.OrderBy);
            }
            else
            {
                ApplyOrderByDescending(criteria.OrderBy);
            }
        }
    }
}