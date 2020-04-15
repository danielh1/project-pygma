using System.Net;
using System.Threading.Tasks;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Data.Domain.Enums;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.Users.ViewModels.Requests;
using Xunit;

namespace Pygma.UatTests.Tests.Flows
{
    public class LoginFlowTest : UatBase
    {
        private readonly HttpTestClient _http;

        public LoginFlowTest(HttpTestClient http)
        {
            _http = http;
        }
        
        [Fact]
        public async Task AttemptLoginWithoutRegistration_Unauthorized()
        {
            await _http
                .DefaultClient
                .LoginAsync(new LoginVm()
                {
                    Email = "withoutRegistration@mymail.com",
                    Password = "1234"
                }, HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task RegisterAndLogin_ShouldSucceed()
        {
            await _http
                .DefaultClient
                .RegisterAsync(new RegisterAccountVm()
                {
                    Email = "registerAndLogin@mymail.com",
                    Password = "1234",
                    Firstname = "Firstname_myTestMail",
                    Lastname = "Lastname_myTestMail"
                });

            await _http
                .DefaultClient
                .LoginAsync(new LoginVm()
                {
                    Email = "registerAndLogin@mymail.com",
                    Password = "1234"
                });
        }
    }
}