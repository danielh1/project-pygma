using System;
using System.Globalization;

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

        public TViewModel Results { get; }

        public int TotalItems { get; }
        public int ItemsPerPage { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
    }
}
