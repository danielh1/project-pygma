using System.Collections.Generic;
using System.Threading.Tasks;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        List<User> LoadForCache();
        new Task<int> CreateAsync(User user);
        Task<List<User>> ReadAllAsync();
        Task<User> ReadByEmailAndPasswordAsync(string email, string password);
        new Task<int> UpdateAsync(User user);
        new Task<int> DeleteAsync(User user);
    }
}