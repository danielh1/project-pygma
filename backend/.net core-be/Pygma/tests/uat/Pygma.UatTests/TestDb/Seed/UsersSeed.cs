using Pygma.Common.Constants;
using Pygma.Data;
using Pygma.Data.Domain.Entities;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.TestDb.Seed
{
    public class UsersSeed : ISeeder
    {
        public void Seed(PygmaDbContext dbContext)
        {
            dbContext.SetInsertIdentity("Users", true);

            CreateUser(dbContext, SeedConstants.AdminUser, "admin", "adminFirstname", "adminLastname", Roles.Admin, true);
            CreateUser(dbContext, SeedConstants.AuthorUser, "author", "authorFirstname", "authorLastname", Roles.Author,true);
            CreateUser(dbContext, SeedConstants.InActiveUser, "inactive", "-", "-", Roles.Author, false);

            dbContext.SaveChanges();

            dbContext.SetInsertIdentity("Users", false);

            dbContext.SaveChanges();
        }

        private static void CreateUser(PygmaDbContext dbContext, int id, string email,  string firstname, string lastname, string role, bool active)
        {
            dbContext.Users.Add(new User()
            {
                Id = id, 
                FirstName = firstname, 
                LastName = lastname,
                Password = "test",
                Role = role,
                Active = active,
                Email = $"{email}@mymail.com"
            });
        }
    }
}