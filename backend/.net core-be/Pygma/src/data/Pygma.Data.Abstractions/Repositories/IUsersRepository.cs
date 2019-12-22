using System.Collections.Generic;
using System.Threading.Tasks;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IUsersRepository : IRepositoryBase<User>
    {
        Task<List<User>> ReadAllAsync();
        Task<User> LoginAsync(string email, string password);
    }
}