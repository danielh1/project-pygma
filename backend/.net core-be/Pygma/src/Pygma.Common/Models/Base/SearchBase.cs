using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Abstractions;
using Pygma.Data.Domain.Enums;

namespace Pygma.Common.Models.Base
{
    public abstract class SearchBase<T> : IPaging
    {
        [FromQuery(Name = "orderBy")]
        [JsonPropertyName("orderBy")]
        public T OrderBy { get; set; }

        [FromQuery(Name = "orderDirection")]
        [JsonPropertyName("orderDirection")]
        public EnOrderByDirection OrderByDirection { get; set; }

        [FromQuery(Name = "currentPage")]
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; } = 1;

        [FromQuery(Name = "pageSize")]
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; } = 20;

        [JsonIgnore]
        public int Skip => (CurrentPage - 1) * PageSize;
        
        [JsonIgnore]
        public int Take => PageSize;
    }
}
