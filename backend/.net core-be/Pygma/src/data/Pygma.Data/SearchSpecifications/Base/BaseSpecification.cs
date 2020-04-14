using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Pygma.Data.Abstractions.Search;
using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.SearchSpecifications.Base
{
     public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public List<Expression<Func<T, bool>>> Criteria { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected void ApplyPagingAndOrder(ISearchCriteria<T> criteria)
        {
            ApplyPaging(criteria.Skip, criteria.Take);

            if (criteria.OrderByByDirection == EnOrderByDirection.Asc)
            {
                ApplyOrderBy(criteria.OrderBy);
            }
            else
            {
                ApplyOrderByDescending(criteria.OrderBy);
            }
        }
        
        private void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        
        private void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        private void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

    }
}