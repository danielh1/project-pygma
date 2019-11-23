using System;
using System.Linq.Expressions;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.Abstractions.Search
{
    public interface ISearchCriteria<T>
    {
        Expression<Func<T, object>> OrderBy { get; set; }
        OrderDirectionEnum OrderDirectionEnum { get; set; }
        int Skip { get; set; }
        int Take { get; set; }
    }
}