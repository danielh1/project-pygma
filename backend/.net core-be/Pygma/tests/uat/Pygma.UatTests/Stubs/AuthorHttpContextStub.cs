namespace Pygma.UatTests.Stubs
{
    // public class AuthorHttpContextStub: IHttpContextAccessor
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
    //         identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "Carpenter@mymail.com"));
    //         var contextUser = new ClaimsPrincipal(identity); //add claims as needed
    //         
    //         var email = contextUser.Identity.GetEmail();
    //         
    //         return new DefaultHttpContext() {
    //             User = contextUser
    //         };
    //     }
    // }
}