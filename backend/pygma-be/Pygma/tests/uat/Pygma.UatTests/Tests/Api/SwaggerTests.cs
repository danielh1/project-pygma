using System.Threading.Tasks;
using Pygma.UatTests.Base;
using Pygma.UatTests.Infrastructure;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class SwaggerTests: UatBase
    {
        private readonly HttpTestClient _http;

        public SwaggerTests(HttpTestClient http)
        {            
            _http = http;
        }

        [Theory]
        [InlineData("/index.html")]
        public async Task Swagger_GetSwaggerIndexAuthorized_ShouldSucceed(string url)
        {
            // Arrange
            var http = _http.DefaultClient;
            
            // Act
            var response = await http.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Contains("text/html", response.Content.Headers.ContentType.ToString());
        }
    }
}