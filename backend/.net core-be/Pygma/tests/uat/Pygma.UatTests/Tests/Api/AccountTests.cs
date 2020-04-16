using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Pygma.Common.Constants;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.Users.ViewModels.Requests;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class AccountTests: UatBase
    {
        private readonly HttpTestClient _http;

        public AccountTests(HttpTestClient http)
        {
            _http = http;
        }

        [Fact]
        public async Task RegisterAccount_ShouldSucceed()
        {
            await _http
                .DefaultClient
                .RegisterAsync(new RegisterAccountVm
                {
                    Firstname = Fixture.Create<string>(),
                    Lastname = Fixture.Create<string>(),
                    Email = "test_register@mymail.com",
                    Password = "test"
                });
        }

        [Fact]
        public async Task RegisterAccount_BadRequest()
        {
            await _http
                .DefaultClient
                .RegisterAsync(null, HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Login_ShouldSucceed()
        {
            var actual = await _http
                .DefaultClient
                .LoginAsync(new LoginVm()
                {
                    Email = "admin@mymail.com",
                    Password = "test"
                });

            actual.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Login_Unauthorized()
        {
            var actual = await _http
                .DefaultClient
                .LoginAsync(new LoginVm()
                {
                    Email = "admin@mymail.com",
                    Password = Fixture.Create<string>()
                }, HttpStatusCode.Unauthorized);

            actual.Should().BeNull();
        }

        [Fact]
        public async Task LoginAdmin_ShouldSucceed()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var user = new LoginVm()
                {
                    Email = "admin@mymail.com",
                    Password = "test"
                };
            
            var actual = await _http
                .DefaultClient
                .LoginAsync(user);
            
            var token = tokenHandler.ReadJwtToken(actual.Token);
            
            var role = token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            
            role.Should().NotBeNullOrEmpty()
                .And.Be(Roles.Admin);
        }

        [Fact]
        public async Task LoginAuthor_ShouldSucceed()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var user = new LoginVm()
            {
                Email = "author@mymail.com",
                Password = "test"
            };
            
            var actual = await _http
                .DefaultClient
                .LoginAsync(user);
            
            var token = tokenHandler.ReadJwtToken(actual.Token);
            
            var role = token.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
            
            role.Should().NotBeNullOrEmpty()
                .And.Be(Roles.Author);
        }
    }
}