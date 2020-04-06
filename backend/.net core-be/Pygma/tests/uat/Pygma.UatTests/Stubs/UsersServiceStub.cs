using System.Collections.Generic;
using System.Security.Claims;
using Pygma.Common.Constants;
using Pygma.Data.Domain.Entities;
using Pygma.UatTests.TestDb.Seed;

namespace Pygma.UatTests.Stubs
{
    // public class AdminsServiceStub: IUsersService
    // {
    //     public User GetUser()
    //     {
    //         return new User()
    //         {
    //             Id = SeedConstants.AdminUser,
    //             FirstName = "-",
    //             LastName = "-",
    //             Email = "a@gmail.com",
    //         };
    //     }
    //
    //     public ClaimsPrincipal GetPrincipalUser()
    //     {
    //         var claim = new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Roles.Admin);
    //         var claimsIdentity = new ClaimsIdentity(new List<Claim>(){ claim });
    //
    //         return new ClaimsPrincipal(claimsIdentity);
    //     }
    //
    //     public int UserId { get; } = SeedConstants.AdminUser;
    // }
    
    // public class AuthorServiceStub: IUsersService
    // {
    //     public User GetUser()
    //     {
    //         return new User()
    //         {
    //             Id = SeedConstants.AuthorUser,
    //             FirstName = "-",
    //             LastName = "-",
    //             Email = "b@gmail.com",
    //         };
    //     }
    //
    //     public ClaimsPrincipal GetPrincipalUser()
    //     {
    //         var claim = new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Roles.Author);
    //         var claimsIdentity = new ClaimsIdentity(new List<Claim>(){ claim });
    //
    //         return new ClaimsPrincipal(claimsIdentity);
    //     }
    //
    //     public int UserId { get; } = SeedConstants.AuthorUser;
    // }
}