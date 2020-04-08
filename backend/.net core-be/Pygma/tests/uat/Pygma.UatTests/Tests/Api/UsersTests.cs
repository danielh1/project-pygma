using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Responses;
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
                .DefaultClient
                .GetAllUsersAsync();

            users.Should().NotBeNullOrEmpty()
                .And.NotContainNulls();
            
            users.Length.Should().Be(3);
            
            var exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.AdminUser));
            Assert.Null(exception);
            exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.AuthorUser));
            Assert.Null(exception);
            exception = Record.Exception(() => users.Single(x => x.Id == SeedConstants.InActiveUser));
            Assert.Null(exception);
        }

        [Fact]
        public async Task GetAll_NotLoggedIn_Unauthorized()
        {
        }

        [Fact]
        public async Task GetAll_Author_Unauthorized()
        {
        }

        [Fact]
        public async Task Get_ShouldSucceed()
        {
        }

        [Fact]
        public async Task Update_ShouldSucceed()
        {
        }

        [Fact]
        public async Task Delete_ShouldSucceed()
        {
        }
    }
}