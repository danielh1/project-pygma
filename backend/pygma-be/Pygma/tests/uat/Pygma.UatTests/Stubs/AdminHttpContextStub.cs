using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Pygma.Common.Constants;

namespace Pygma.UatTests.Stubs
{
    public class AdminHttpContextStub: IHttpContextAccessor
    {
        public HttpContext HttpContext
        {
            get => GetStub();
            set { }
        }

        private static DefaultHttpContext GetStub()
        {
            var identity = new GenericIdentity("some name", "test");
            
            identity.AddClaim(new Claim(ClaimTypes.Email, "admin@mymail.com"));
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Admin));
            var contextUser = new ClaimsPrincipal(identity);
            
            return new DefaultHttpContext() {
                User = contextUser
            };
        }
    }
}