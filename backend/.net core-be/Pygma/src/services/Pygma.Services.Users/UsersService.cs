using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Pygma.Data.Abstractions.Cache;
using Pygma.Data.Domain.Entities;
using Pygma.Services.Users.Extensions;

namespace Pygma.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersCache _usersCache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(IUsersCache usersCache,
            IHttpContextAccessor httpContextAccessor)
        {
            _usersCache = usersCache;
            _httpContextAccessor = httpContextAccessor;
        }

        private User SetApplicationUser()
        {
            var email = _httpContextAccessor.HttpContext.User.Identity.GetEmail();

            return string.IsNullOrEmpty(email)
                ? null
                : _usersCache.GetByEmail(email);
        }

        public ClaimsPrincipal GetPrincipalUser() => _httpContextAccessor.HttpContext.User;

        public User GetUser() => SetApplicationUser();

        public int UserId => GetUser().Id;
    }
}