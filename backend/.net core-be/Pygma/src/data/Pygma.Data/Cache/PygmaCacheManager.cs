using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pygma.Data.Domain.Entities.Base;

namespace Pygma.Data.Cache
{
    public abstract class PygmaCacheManager<TEntity> where TEntity : EntityBase
    {
        protected readonly IMemoryCache Cache;
        protected readonly string Key;

        protected const string List = "LIST";
        protected const string Dictionary = "DICTIONARY";

        protected PygmaCacheManager(IMemoryCache cache, string key)
        {
            Cache = cache;
            Key = key;
        }

        public List<TEntity> GetAll()
        {
            return Cache.TryGetValue(List + Key, out List<TEntity> result) ? result : null;
        }

        public TEntity GetById(int id)
        {
            return Cache.TryGetValue(Dictionary + Key, out Dictionary<int, TEntity> dictionary)
                ? (dictionary.TryGetValue(id, out var result) ? result : null)
                : null;
        }

        public void Invalidate(List<TEntity> entities)
        {
            Cache.Set(List + Key, entities);
            Cache.Set(Dictionary + Key, entities.ToDictionary(x => x.Id, x => x));
        }
    }
}
