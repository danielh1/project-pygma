using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Abstractions;
using Pygma.Data.Domain.Enums;

namespace Pygma.Common.Models.Base
{
    public abstract class SearchFiltersBase<T> : IPaging
    {
        [FromQuery(Name = "orderBy")]
        public T OrderBy { get; set; }

        [FromQuery(Name = "orderDirection")]
        public EnOrderDirection EnOrderDirection { get; set; }

        [FromQuery(Name = "currentPage")]
        public int CurrentPage { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 20;
    }
}
