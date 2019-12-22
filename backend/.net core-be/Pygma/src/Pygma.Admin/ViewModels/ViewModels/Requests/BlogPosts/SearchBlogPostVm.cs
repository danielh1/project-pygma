using System;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Base;

namespace Pygma.Admin.ViewModels.ViewModels.Requests.BlogPosts
{
    public enum EnBlogPostOrderField
    {        
        Title,
        PublishedAtFrom,
        PublishedAtTo
    }

    public class SearchBlogPostVm : SearchFiltersBase<EnBlogPostOrderField>
    {
        [FromQuery(Name = "title")]
        public string Title { get; set; }
        
        [FromQuery(Name = "publishedAtFrom")]
        public DateTime? PublishedAtFrom { get; set; }

        [FromQuery(Name = "publishedAtTo")]
        public DateTime? PublishedAtTo { get; set; }
    }
}