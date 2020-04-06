using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Pygma.Data.Abstractions.Cache;
using Pygma.Data.Domain.Entities;

namespace Pygma.Data.Cache
{
    public class UsersCache : PygmaCacheManager<User>, IUsersCache
    {
        public UsersCache(IMemoryCache cache): base(cache, CacheKeys.Users)
        {
        }

        public User GetByEmail(string email)
        {
            return Cache.TryGetValue(Dictionary + "_EMAIL" + Key, out Dictionary<string, User> dictionary)
                ? (dictionary.TryGetValue(email, out var result) ? result : null)
                : null;
        }

        public new void Invalidate(List<User> entities)
        {
            Cache.Set(List + Key, entities);
            Cache.Set(Dictionary + Key, entities.ToDictionary(x => x.Id, x => x));
            Cache.Set(Dictionary + "_EMAIL" + Key, entities.ToDictionary(x => x.Email, x => x));
        }
    }
}

