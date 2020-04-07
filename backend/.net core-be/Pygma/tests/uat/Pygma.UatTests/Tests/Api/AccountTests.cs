using System.Threading.Tasks;
using Pygma.UatTests.Base;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
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
            var actual = await _http
                .DefaultClient
                ;

            actual.Results.Should().NotBeEmpty();
            actual.Results.Length.Should().Be(1);
            actual.Results.Single().LastName.Should().Be("CarpenterWithSalesPerson");
            actual.Results.Single().Id.Should().Be(SeedConstants.CarpenterPartner);
        }
        
        [Fact]
        public async Task RegisterAccount_BadRequest()
        {
            var actual = await _http
                    .DefaultClient
                ;

            actual.Results.Should().NotBeEmpty();
            actual.Results.Length.Should().Be(1);
            actual.Results.Single().LastName.Should().Be("CarpenterWithSalesPerson");
            actual.Results.Single().Id.Should().Be(SeedConstants.CarpenterPartner);
        }

        
        [Fact]
        public async Task Login_ShouldSucceed()
        {
            var actual = await _http
                    .DefaultClient
                ;

            actual.Results.Should().NotBeEmpty();
            actual.Results.Length.Should().Be(1);
            actual.Results.Single().LastName.Should().Be("CarpenterWithSalesPerson");
            actual.Results.Single().Id.Should().Be(SeedConstants.CarpenterPartner);
        }
        
        [Fact]
        public async Task Login_Unauthorized()
        {
            var actual = await _http
                    .DefaultClient
                ;

            actual.Results.Should().NotBeEmpty();
            actual.Results.Length.Should().Be(1);
            actual.Results.Single().LastName.Should().Be("CarpenterWithSalesPerson");
            actual.Results.Single().Id.Should().Be(SeedConstants.CarpenterPartner);
        }
    }
}