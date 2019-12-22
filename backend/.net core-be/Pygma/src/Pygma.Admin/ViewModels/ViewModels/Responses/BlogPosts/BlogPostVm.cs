using System;
using Pygma.Common.Abstractions;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Domain.Enums;

namespace Pygma.Admin.ViewModels.ViewModels.Responses.BlogPosts
{
    public class BlogPostVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public EnBlogPostStatus Status { get; set; }
        public IBaseVm Author { get; set; }
        public DateTime? PublishedAt { get; set; }
        public BlogPostCommentVm[] BlogPostComments { get; set; }
    }
}