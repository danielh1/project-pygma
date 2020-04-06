using Pygma.Data;

namespace Pygma.UatTests.TestDb
{
    public static class DbExtensions
    {
        public static PygmaDbContext CreateSqlDbContext() 
        {         
            var db = (new DbContextFactory()).CreateContextForSqServer();
            SeedData.PopulateTestData(db);
            
            return db;
         }
    }
}