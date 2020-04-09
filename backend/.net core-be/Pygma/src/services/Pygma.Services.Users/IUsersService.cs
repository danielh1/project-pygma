using System.Security.Claims;
using Pygma.Data.Domain.Entities;

namespace Pygma.Services.Users
{
    public interface IUsersService
    {
        User GetUser();

        ClaimsPrincipal GetPrincipalUser();

        int UserId { get; }
    }
}