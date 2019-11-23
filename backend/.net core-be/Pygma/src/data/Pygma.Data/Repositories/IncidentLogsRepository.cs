using Microsoft.EntityFrameworkCore;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Repositories.Base;

namespace Pygma.Data.Repositories
{
    public class IncidentLogsRepository : RepositoryBase<IncidentLog>, IIncidentLogsRepository
    {
        public IncidentLogsRepository(DbContext context) : base(context)
        {
            
        }
    }
}