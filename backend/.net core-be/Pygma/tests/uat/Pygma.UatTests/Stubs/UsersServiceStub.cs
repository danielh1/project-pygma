using System.Collections.Generic;
using System.Security.Claims;
using Pygma.Common.Constants;
using Pygma.Data.Domain.Entities;
using Pygma.Services.Users;
using Pygma.UatTests.TestDb.Seed;

namespace Pygma.UatTests.Stubs
{
    // public class AdminServiceStub: IUsersService
    // {
    //     public User GetUser()
    //     {
    //         return new User()
    //         {
    //             Id = SeedConstants.AdminUser,
    //             FirstName = "-",
    //             LastName = "-",
    //             Email = "admin@mymail.com",
    //         };
    //     }
    //
    //     public ClaimsPrincipal GetPrincipalUser()
    //     {
    //         var claim = new Claim(ClaimTypes.Role, Roles.Admin);
    //         var claimsIdentity = new ClaimsIdentity(new List<Claim>(){ claim });
    //
    //         return new ClaimsPrincipal(claimsIdentity);
    //     }
    //
    //     public int UserId { get; } = SeedConstants.AdminUser;
    // }
    //
    // public class AuthorServiceStub: IUsersService
    // {
    //     public User GetUser()
    //     {
    //         return new User()
    //         {
    //             Id = SeedConstants.AuthorUser,
    //             FirstName = "-",
    //             LastName = "-",
    //             Email = "b@mymail.com",
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