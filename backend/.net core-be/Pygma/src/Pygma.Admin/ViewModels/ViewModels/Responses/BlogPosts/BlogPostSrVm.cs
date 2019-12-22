using System;

namespace Pygma.Admin.ViewModels.ViewModels.Responses.BlogPosts
{
    public class BlogPostSrVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public DateTime? PublishedAt { get; set; }
    }
}