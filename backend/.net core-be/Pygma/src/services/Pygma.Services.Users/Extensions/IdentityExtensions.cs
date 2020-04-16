using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Pygma.Services.Users.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetEmail(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);

            return claim?.Value ?? string.Empty;
        }

        public static string GetName(this IIdentity identity)
        {
            var claimsIdentity = identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }
    }
}
