using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pygma.App;
using Pygma.Data;
using Pygma.UatTests.TestDb;

namespace Pygma.UatTests
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(ConfigureTestServices);
            builder.ConfigureLogging((WebHostBuilderContext context, ILoggingBuilder loggingBuilder) =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddConsole(options => options.IncludeScopes = true);
            });
        }

        protected void ConfigureTestServices(IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, DisableAuthorizationHandler<IAuthorizationRequirement>>();

            #region Db
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<PygmaDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<PygmaDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database={Pygma_Test;Integrated Security=SSPI");
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database contexts
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var appDb = scopedServices.GetRequiredService<PygmaDbContext>();

                try
                {
                    appDb.Database.EnsureDeleted();
                    appDb.Database.EnsureCreated();
                    
                    // Seed the database with some specific test data.
                    SeedData.PopulateTestData(appDb);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            #endregion
        }
    }
    
    public class DisableAuthorizationHandler<TRequirement> : AuthorizationHandler<TRequirement>
        where TRequirement : IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}