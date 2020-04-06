using Pygma.Data;
using Pygma.Data.Domain.Entities;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.TestDb.Seed
{
    public class UsersSeed: ISeeder
    {
        public void Seed(PygmaDbContext dbContext)
        {
            dbContext.SetInsertIdentity("Users", true);
            
            CreateUser(dbContext, SeedConstants.AdminUser, "adminFirstname", "adminLastname", true);
            CreateUser(dbContext, SeedConstants.AuthorUser, "authorFirstname", "authorLastname", true);
            CreateUser(dbContext, SeedConstants.InActiveUser, "-", "-", false);

            dbContext.SaveChanges();
            
            dbContext.SetInsertIdentity("Users", false);
            
            dbContext.SaveChanges();
        }

        private void CreateUser(PygmaDbContext dbContext, int id, string firstname, string lastname, bool active)
        {
            dbContext.Users.Add(new User()
            {
                Id = id, FirstName = firstname, LastName = lastname, Active = active,
                Email = $"{firstname}@gmail.com" 
            });
        }
    }
}