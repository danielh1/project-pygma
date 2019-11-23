using System.Collections.Generic;

namespace Pygma.Data.Abstractions.Cache
{
    public interface IPygmaCache<TEntity>
    {
        List<TEntity> GetAll();

        TEntity GetById(int id);

        void Invalidate(List<TEntity> entities);
    }
}