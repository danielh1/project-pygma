using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class BlogPostCommentsRepository : RepositoryBase<Comment>, IBlogPostCommentsRepository
    {
        public BlogPostCommentsRepository(PygmaDbContext context) : base(context)
        {
        }

        public Task<Comment> ReadByIdAndBlogPostIdAsync(int id, int blogPostId)
        {
            return DbTable
                .Where(x => x.Id == id && x.BlogPostId == blogPostId)
                .FirstOrDefaultAsync();
        }
    }
}