using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Pygma.UatTests.Extensions;
using Pygma.UatTests.Stubs;

namespace Pygma.UatTests.Infrastructure
{
    public class HttpTestClient
    {
        public readonly TestWebApplicationFactory TestWebApplicationFactory;

        public readonly HttpClient DefaultClient;
        public readonly HttpClient AdminClient;
        public readonly HttpClient AuthorClient;
        public readonly HttpClient InactiveAuthorClient;

        public HttpTestClient()
        {
            TestWebApplicationFactory = new TestWebApplicationFactory();
            DefaultClient = GetDefaultClient();
            AdminClient = GetAdminClient();
            AuthorClient = GetAuthorClient();
            InactiveAuthorClient = GetInactiveAuthorClient();
        }

        private HttpClient GetDefaultClient()
        {
            var client = TestWebApplicationFactory
                .CreateClient();

            return client;
        }

        private HttpClient GetAdminClient()
        {
            var client = TestWebApplicationFactory
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureTestServices(services =>
                        {
                            services.RemoveRegisteredType(typeof(HttpContextAccessor));
                            var context = new AdminHttpContextStub();
                            services.AddSingleton<IHttpContextAccessor, AdminHttpContextStub>(s => context);
                        });
                    }
                )
                .CreateClient();

            return client;
        }

        private HttpClient GetAuthorClient()
        {
            var client = TestWebApplicationFactory
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureTestServices(services =>
                        {
                            services.RemoveRegisteredType(typeof(HttpContextAccessor));
                            var context = new AuthorHttpContextStub();
                            services.AddSingleton<IHttpContextAccessor, AuthorHttpContextStub>(s => context);
                        });
                    }
                )
                .CreateClient();

            return client;
        }
        
        private HttpClient GetInactiveAuthorClient()
        {
            var client = TestWebApplicationFactory
                .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureTestServices(services =>
                        {
                            services.RemoveRegisteredType(typeof(HttpContextAccessor));
                            var context = new InactiveAuthorHttpContextStub();
                            services.AddSingleton<IHttpContextAccessor, InactiveAuthorHttpContextStub>(s => context);
                        });
                    }
                )
                .CreateClient();

            return client;
        }
    }
}