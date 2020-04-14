using System;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Base;
using Pygma.Data.Domain.Enums;

namespace Pygma.Blog.ViewModels.Requests.BlogPosts
{
    public enum EnBlogPostOrderField
    {        
        Title,
        PublishedAtFrom,
        PublishedAtTo
    }

    public class SearchBlogPostVm : SearchBase<EnBlogPostOrderField>
    {
        [FromQuery(Name = "title")]
        public string Title { get; set; }
        
        [FromQuery(Name = "status")]
        public EnBlogPostStatus? Status { get; set; }
        
        [FromQuery(Name = "publishedAtFrom")]
        public DateTime? PublishedAtFrom { get; set; }

        [FromQuery(Name = "publishedAtTo")]
        public DateTime? PublishedAtTo { get; set; }
        
        [FromQuery(Name = "authorLastname")]
        public string AuthorLastname { get; set; }
    }
}