using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class BlogPostsRepository : RepositoryBase<BlogPost>, IBlogPostsRepository
    {
        public BlogPostsRepository(DbContext context) : base(context)
        {
        }
    }
}