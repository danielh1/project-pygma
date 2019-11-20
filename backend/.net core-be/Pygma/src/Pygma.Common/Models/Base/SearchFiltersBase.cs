using Microsoft.AspNetCore.Mvc;
using TrFoil.Backbone.Common.Abstractions;
using TrFoil.Backbone.Domain.Enums;

namespace TrFoil.Backbone.Common.Models.Base
{
    public abstract class SearchFiltersBase<T> : IPaging
    {
        [FromQuery(Name = "orderBy")]
        public T OrderBy { get; set; }

        [FromQuery(Name = "orderDirection")]
        public OrderDirectionEnum OrderDirection { get; set; }

        [FromQuery(Name = "currentPage")]
        public int CurrentPage { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 20;
    }
}
