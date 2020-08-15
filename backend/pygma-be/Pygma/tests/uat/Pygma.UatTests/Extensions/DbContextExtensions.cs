using Microsoft.EntityFrameworkCore;
using Pygma.Data;

namespace Pygma.UatTests.Extensions
{
    public static class DbContextExtensions
    {
        public static void SetInsertIdentity(this PygmaDbContext dbContext, string table, bool on)
        {
            dbContext.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {table} {(on ? "ON" : "OFF")};");
        }
    }
}