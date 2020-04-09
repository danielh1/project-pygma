using Pygma.Data.Abstractions.Cache;
using Pygma.Data.Abstractions.Repositories;

namespace Pygma.Services.Bootstrapper
{
    public class BootstrapCacheService: IBootstrapCacheService
    {
        private readonly IUsersCache _usersCache;
        private readonly IUsersRepository _usersRepository;

        public BootstrapCacheService(
            IUsersCache usersCache,
            IUsersRepository usersRepository
        )
        {
            _usersCache = usersCache;
            _usersRepository = usersRepository;
        }

        public void Invalidate()
        {
            _usersCache.Invalidate(_usersRepository.LoadForCache());
        }
    }
}
