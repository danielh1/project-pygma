using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Pygma.Common.Models.Search
{
    public class SearchResultsVm<TViewModel> where TViewModel : class
    {
        public SearchResultsVm()
        {
            
        }

        public SearchResultsVm(
            TViewModel results,
            int totalItems,
            int itemsPerPage,
            int currentPage,
            int pageSize
        )
        {
            Results = results;
            TotalItems = totalItems;
            ItemsPerPage = itemsPerPage;
            CurrentPage = currentPage;

            TotalPages = int.Parse(Math.Ceiling((decimal) totalItems / pageSize)
                .ToString(CultureInfo.InvariantCulture));

        }

        [JsonPropertyName("results")]
        public TViewModel Results { get; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; }
        
        [JsonPropertyName("itemsPerPage")]
        public int ItemsPerPage { get; }
        
        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; }
        
        [JsonPropertyName("totalPages")]
        public int TotalPages { get; }
    }
}
