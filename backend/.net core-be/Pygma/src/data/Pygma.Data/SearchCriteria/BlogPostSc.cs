using System;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Domain.Enums;
using Pygma.Data.SearchCriteria.Base;

namespace Pygma.Data.SearchCriteria
{
    public class BlogPostSc : SearchCriteriaBase<BlogPost>
    {
        public string Title { get; set; }
        public string Post { get; set; }
        public DateTime? PublishedAtFrom { get; set; }
        public DateTime? PublishedAtTo { get; set; }
        public EnBlogPostStatus? Status { get; set; }
    }
}