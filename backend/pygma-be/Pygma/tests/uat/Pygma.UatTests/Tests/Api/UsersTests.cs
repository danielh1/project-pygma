using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Requests.Account;
using Pygma.Users.ViewModels.Requests.Users;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class UsersTests : UatBase
    {
        private readonly HttpTestClient _http;

        public UsersTests(HttpTestClient http)
        {
            _http = http;
        }

        [Fact]
        public async Task GetAll_ShouldSucceed()
        {
            var users = await _http
                .AdminClient
                .GetAllUsersAsync();

            users.Should().NotBeNullOrEmpty()
                .And.NotContainNulls();

            var exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.AdminUser));
            Assert.Null(exception);
            exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.AuthorUser));
            Assert.Null(exception);
            exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.InActiveUser));
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAll_NotLoggedIn_Forbidden()
        {
            await _http
                .DefaultClient
                .GetAllUsersAsync(HttpStatusCode.Forbidden);
        }

        [Fact]
        public async Task GetAll_InActiveAuthor_Unauthorized()
        {
            await _http
                .InactiveAuthorClient
                .GetAllUsersAsync(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Get_ShouldSucceed()
        {
            var user = await _http
                .AdminClient
                .GetUserAsync(SeedConstants.AuthorUser);

            user.Id.Should().Be(SeedConstants.AuthorUser);
        }

        [Fact]
        public async Task Update_ShouldSucceed()
        {
            await _http.AdminClient
                .RegisterAsync(new RegisterAccountVm()
                {
                    Email = "test@test.com",
                    Firstname = "test",
                    Lastname = "test",
                    Password = "1234"
                });

            var newUser = (await _http.AdminClient
                    .GetAllUsersAsync())
                .FirstOrDefault(x => x.Email == "test@test.com");

            Debug.Assert(newUser != null, nameof(newUser) + " != null");

            newUser.FirstName.Should().Be("test");
            newUser.LastName.Should().Be("test");
            newUser.Active.Should().BeFalse();

            await _http.AdminClient
                .UpdateUserAsync(newUser.Id, new UpdateUserVm()
                {
                    Active = true,
                    Firstname = "Test updated firstname",
                    Lastname = "Test updated lastname"
                });

            var updatedUser = (await _http.AdminClient
                .GetUserAsync(newUser.Id));

            updatedUser.Active.Should().BeTrue();
            updatedUser.FirstName.Should().Be("Test updated firstname");
            updatedUser.LastName.Should().Be("Test updated lastname");
        }

        [Fact]
        public async Task Delete_ShouldSucceed()
        {
            await _http.AdminClient
                .RegisterAsync(new RegisterAccountVm()
                {
                    Firstname = "test",
                    Lastname = "test",
                    Email = "test_delete@test.com",
                    Password = "1234"
                });

            var newUser = (await _http.AdminClient
                    .GetAllUsersAsync())
                .FirstOrDefault(x => x.Email == "test_delete@test.com");

            Debug.Assert(newUser != null, nameof(newUser) + " != null");

            await _http.AdminClient
                .DeleteUserAsync(newUser.Id);

            newUser = (await _http.AdminClient
                    .GetAllUsersAsync())
                .FirstOrDefault(x => x.Email == "test_delete@test.com");

            newUser.Should().BeNull();
        }
    }
}