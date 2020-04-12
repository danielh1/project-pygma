using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Abstractions.SearchSpecifications;
using Pygma.Data.Domain.Entities.Base;
using Pygma.Data.SearchSpecifications;

namespace Pygma.Data.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase, new()
    {
        private readonly DbContext _dbContext;

        protected DbSet<TEntity> DbTable { get; }

        protected RepositoryBase(DbContext context)
        {
            _dbContext = context;
            DbTable = _dbContext.Set<TEntity>();
        }

        public async Task<int> CreateAsync(TEntity user)
        {
            DbTable.Add(user);
            return await SaveChangesAsync();
        }

        public async Task<int> CreateAsync(IEnumerable<TEntity> entities)
        {
            DbTable.AddRange(entities);
            return await SaveChangesAsync();
        }

        public ValueTask<TEntity> ReadByIdAsync(int id) => DbTable.FindAsync(id);

        public Task<List<TEntity>> ReadAsync(Expression<Func<TEntity, bool>> where)
            => DbTable.Where(where).ToListAsync();
        
        public async Task<int> UpdateAsync(TEntity user)
        {
            user.UpdatedAt = DateTime.Now;

            DbTable.Update(user);
            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            var updatedAt = DateTime.Now;

            foreach (var entity in entities)
            {
                entity.UpdatedAt = updatedAt;
            }

            DbTable.UpdateRange(entities);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = DbTable.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                DbTable.Remove(entity);    
            }
            
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(TEntity user)
        {
            _dbContext.Entry(user).State = EntityState.Deleted;
            return await SaveChangesAsync();
        }
        
        public async Task<IReadOnlyList<TEntity>> SearchAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec, false).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec, bool pagingLockOpen = true)
        {
            return (new SpecificationEvaluator<TEntity>()).GetQuery(DbTable.AsQueryable(), spec, pagingLockOpen);
        }

        protected async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            catch (DbUpdateException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}