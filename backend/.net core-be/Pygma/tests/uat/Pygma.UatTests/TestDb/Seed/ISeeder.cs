using Pygma.Data;

namespace Pygma.UatTests.TestDb.Seed
{
    public interface ISeeder
    {
        void Seed(PygmaDbContext dbContext);
    }
}