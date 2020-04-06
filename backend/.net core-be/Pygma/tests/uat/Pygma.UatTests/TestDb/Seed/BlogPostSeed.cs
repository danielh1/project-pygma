using Pygma.Data;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.TestDb.Seed
{
    public class BlogPostSeed: ISeeder
    {
        public void Seed(PygmaDbContext dbContext)
        {
            dbContext.SetInsertIdentity("Suppliers", true);
            
            // var supplier = new Supplier() { Id = SeedConstants.Supplier};
            // dbContext.Suppliers.Add(supplier);
            //
            dbContext.SaveChanges();
            
            dbContext.SetInsertIdentity("Suppliers", false);
            
            dbContext.SaveChanges();
        }
    }
}