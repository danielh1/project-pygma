using System.Threading.Tasks;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IBlogPostCommentsRepository : IRepositoryBase<BlogPostComment>
    {
        Task<BlogPostComment> ReadByIdAndBlogPostIdAsync(int id, int blogPostId);
    }
}