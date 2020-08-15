using System.Threading.Tasks;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Models.Search;
using Pygma.Data.SearchCriteria;

namespace Pygma.Blog.Services.Abstractions
{
    public interface IBlogPostsService
    {
        Task<SearchResultsVm<BlogPostSrVm[]>> SearchBlogPostsAsync(BlogPostSc sc);
    }
}