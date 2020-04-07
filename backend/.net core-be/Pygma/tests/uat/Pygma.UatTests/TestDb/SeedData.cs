using System.Collections.Generic;
using Pygma.Data;
using Pygma.UatTests.TestDb.Seed;

namespace Pygma.UatTests.TestDb
{
    public static class SeedData
    {
        public static void PopulateTestData(PygmaDbContext dbContext)
        {
            using var transaction = dbContext.Database.BeginTransaction();
            var seeding = new List<ISeeder>()
            {
                new UsersSeed(),
                //new BlogPostSeed(),
                //new BlogPostCommentSeed(),
            };

            foreach (var seeder in seeding)
            {
                seeder.Seed(dbContext);
            }
                
            dbContext.SaveChanges();
            transaction.Commit();
        }
    }
}