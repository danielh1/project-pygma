using System.Net.Http;

namespace Pygma.UatTests.Infrastructure
{
    public class HttpTestClient
    {
        public readonly TestWebApplicationFactory TestWebApplicationFactory;
    
        public readonly HttpClient DefaultClient;
        public readonly HttpClient AdminClient;
        public readonly HttpClient AuthorClient;
    
        public HttpTestClient()
        {
            TestWebApplicationFactory = new TestWebApplicationFactory();
            DefaultClient = GetDefaultClient();
            AdminClient = GetAdminClient();
            AuthorClient = GetAuthorClient();
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