using System;
using System.Linq.Expressions;
using Pygma.Data.Abstractions.Search;
using Pygma.Data.Domain.Enums;

namespace Pygma.Data.SearchCriteria.Base
{
    public abstract class SearchCriteriaBase<T> : ISearchCriteria<T>
    {
        public Expression<Func<T, object>> OrderBy { get; set; }
        public EnOrderByDirection OrderByByDirection { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public int CurrentPage { get; set; }
    }
}