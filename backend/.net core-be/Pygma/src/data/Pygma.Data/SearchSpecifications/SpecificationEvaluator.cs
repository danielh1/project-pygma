using System.Linq;
using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Entities.Base;

namespace Pygma.Data.SearchSpecifications
{
    public class SpecificationEvaluator<T> where T : EntityBase
    {
        public IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification, bool pagingLockOpen = true)
        {
            var query = inputQuery;
            
            query = specification.Criteria.Aggregate(query,
                (current, where) => current.Where(where));

            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (pagingLockOpen && specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }
    }
}