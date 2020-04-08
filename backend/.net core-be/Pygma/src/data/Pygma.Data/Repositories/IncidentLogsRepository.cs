using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class IncidentLogsRepository : RepositoryBase<Log>, IIncidentLogsRepository
    {
        public IncidentLogsRepository(PygmaDbContext context) : base(context)
        {
            
        }
    }
}