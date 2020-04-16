using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Pygma.Common.Constants;

namespace Pygma.UatTests.Stubs
{
    public class AuthorHttpContextStub: IHttpContextAccessor
    {
        public HttpContext HttpContext
        {
            get => GetStub();
            set { }
        }

        private static DefaultHttpContext GetStub()
        {
            var identity = new GenericIdentity("some name", "test");
            
            identity.AddClaim(new Claim(ClaimTypes.Email, "author@mymail.com"));
            identity.AddClaim(new Claim(ClaimTypes.Role, Roles.Author));
            var contextUser = new ClaimsPrincipal(identity); //add claims as needed

            return new DefaultHttpContext() {
                User = contextUser
            };
        }
    }
}