using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Pygma.Data.Abstractions.SearchSpecifications;

namespace Pygma.Data.Abstractions.Repositories
{
    public interface IRepositoryBase<TEntity>
    {
        Task<int> CreateAsync(TEntity user);
        Task<int> CreateAsync(IEnumerable<TEntity> entities);
        ValueTask<TEntity> ReadByIdAsync(int id);
        Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> where);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(IEnumerable<TEntity> entities);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(TEntity user);
        
        Task<IReadOnlyList<TEntity>> SearchAsync(ISpecification<TEntity> spec);
        Task<int> CountAsync(ISpecification<TEntity> spec);
    }
}