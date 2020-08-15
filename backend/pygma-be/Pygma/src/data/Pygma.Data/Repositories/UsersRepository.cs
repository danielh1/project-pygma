using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Cache;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class UsersRepository : RepositoryBase<User>, IUsersRepository
    {
        private readonly PygmaDbContext _context;
        private readonly IUsersCache _usersCache;
        
        public UsersRepository(PygmaDbContext context, IUsersCache usersCache) : base(context)
        {
            _context = context;
            _usersCache = usersCache;
        }
        
        public List<User> LoadForCache() => DbTable.ToList();
        
        public new async Task<int> CreateAsync(User user)
        {
            DbTable.Add(user);
            
            var result = await SaveChangesAsync();

            _usersCache.Invalidate((await ReadAllAsync()).ToList());

            return result;
        }
        
        public Task<List<User>> ReadAllAsync() => DbTable.ToListAsync();
        
        public Task<User> ReadByEmailAndPasswordAsync(string email, string password)
        {
            return DbTable.Where(x => x.Email == email && x.Password == password).FirstOrDefaultAsync();
        }
        
        public new async Task<int> UpdateAsync(User entity)
        {
            entity.UpdatedAt = DateTime.Now;
            
            DbTable.Update(entity);

            var result = await SaveChangesAsync();

            _usersCache.Invalidate((await ReadAllAsync()).ToList());

            return result;
        }
        
        public new async Task<int> DeleteAsync(User user)
        {
            _context.Entry(user).State = EntityState.Deleted;
            var result= await SaveChangesAsync();

            _usersCache.Invalidate((await ReadAllAsync()).ToList());

            return result;
        }
    }
}