using System.Threading.Tasks;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IBlogPostsRepository : IRepositoryBase<BlogPost>
    {
        Task<BlogPost> ReadByIdWithCommentsAsync(int id);
    }
}
