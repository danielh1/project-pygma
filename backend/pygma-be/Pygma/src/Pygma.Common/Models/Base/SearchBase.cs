using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Abstractions;
using Pygma.Data.Domain.Enums;

namespace Pygma.Common.Models.Base
{
    public abstract class SearchBase<T> : IPaging
    {
        [FromQuery(Name = "orderBy")]
        public T OrderBy { get; set; }

        [FromQuery(Name = "orderDirection")]
        public EnOrderByDirection OrderByDirection { get; set; }

        [FromQuery(Name = "currentPage")]
        public int CurrentPage { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 20;

        [JsonIgnore]
        public int Skip => (CurrentPage - 1) * PageSize;
        
        [JsonIgnore]
        public int Take => PageSize;
    }
}
