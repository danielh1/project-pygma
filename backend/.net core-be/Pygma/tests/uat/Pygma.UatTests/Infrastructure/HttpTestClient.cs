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
    
        public readonly HttpClient AdminClient;
        public readonly HttpClient AuthorClient;
    
        public HttpTestClient()
        {
            TestWebApplicationFactory = new TestWebApplicationFactory();
            AdminClient = GetAdminClient();
            AuthorClient = GetAuthorClient();
        }
    
        private HttpClient GetAdminClient()
        {
            var client = TestWebApplicationFactory
                .WithWebHostBuilder(builder =>
                    {
                        // builder.ConfigureTestServices(services =>
                        // {
                        //     services.RemoveRegisteredType(typeof(UsersService)); // Seems not honored
                        //     services.AddSingleton<IUsersService, CarpenterServiceStub>(s => new CarpenterServiceStub());
                        //     
                        //     services.RemoveRegisteredType(typeof(HttpContextAccessor));
                        //     var context = new AuthorHttpContextStub();
                        //     services.AddSingleton<IHttpContextAccessor, AuthorHttpContextStub>(s => context);
                        // });
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
                        // builder.ConfigureTestServices(services =>
                        // {
                        //     services.RemoveRegisteredType(typeof(UsersService)); // Seems not honored
                        //     services.AddSingleton<IUsersService, FactoryServiceStub>(s => new FactoryServiceStub());
                        //     
                        //     services.RemoveRegisteredType(typeof(HttpContextAccessor));
                        //     var context = new AdminHttpContextStub();
                        //     services.AddSingleton<IHttpContextAccessor, AdminHttpContextStub>(s => context);
                        // });
                    }
                )
                .CreateClient();
    
            return client;
        }
    }
}