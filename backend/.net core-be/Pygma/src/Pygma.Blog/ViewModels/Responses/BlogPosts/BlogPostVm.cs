using System;
using Pygma.Data.Domain.Entities;

namespace Pygma.Blog.ViewModels.Responses.BlogPosts
{
    public class BlogPostVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Post { get; set; }
        public DateTime? PublishedAt { get; set; }
        public BlogPostComment[] BlogPostComments { get; set; }
    }
}