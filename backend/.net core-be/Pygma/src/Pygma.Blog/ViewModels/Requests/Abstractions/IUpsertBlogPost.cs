using Pygma.Data.Domain.Enums;

namespace Pygma.Blog.ViewModels.Requests.Abstractions
{
    public interface IUpsertBlogPost
    {
        public string Title { get; set; }
        
        public string Post { get; set; }
        
        public EnBlogPostStatus Status { get; set; }
    }
}