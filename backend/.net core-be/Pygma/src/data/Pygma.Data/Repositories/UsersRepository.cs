using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(PygmaDbContext context) : base(context)
        {
        }
        
        public Task<List<User>> ReadAllAsync() => DbTable.ToListAsync();
        public Task<User> LoginAsync(string email, string password)
        {
            return DbTable.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }
    }
}