using System;
using Pygma.Data.Domain.Enums;

namespace Pygma.Blog.ViewModels.Requests.BlogPosts
{
    public class CreateBlogPostVm
    {
        public string Title { get; set; }
        
        public string Post { get; set; }
        
        public EnBlogPostStatus Status { get; set; }
    }
}