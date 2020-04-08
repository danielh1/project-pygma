namespace Pygma.UatTests.Stubs
{
    // public class AdminHttpContextStub: IHttpContextAccessor
    // {
    //     private HttpContext _httpContext;
    //     
    //     public HttpContext HttpContext
    //     {
    //         get => GetStub();
    //         set => _httpContext = value;
    //     }
    //
    //     private static DefaultHttpContext GetStub()
    //     {
    //         var identity = new GenericIdentity("some name", "test");
    //         var contextUser = new ClaimsPrincipal(identity); //add claims as needed
    //         
    //         // var claimsIdentity = new ClaimsIdentity(new List<Claim>(){ claim });
    //         
    //         identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "admin@mymail.com"));
    //         identity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Roles.Admin));
    //         
    //         var email = contextUser.Identity.GetEmail();
    //         
    //         return new DefaultHttpContext() {
    //             User = contextUser
    //         };
    //     }
    // }
}