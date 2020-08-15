using System.Threading.Tasks;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IBlogPostCommentsRepository : IRepositoryBase<Comment>
    {
        Task<Comment> ReadByIdAndBlogPostIdAsync(int id, int blogPostId);
    }
}