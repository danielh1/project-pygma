using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pygma.App.Autofac.Modules;


namespace Pygma.App.Extensions
{
    public static class AutofacServiceExtensions
    {
        public static IServiceProvider AddAutofacServiceProvider(this IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            IContainer container = null;
            containerBuilder.Register(c => container).As<IContainer>().SingleInstance();

            var runtimeAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            containerBuilder.RegisterModule(new ServiceModule(runtimeAssemblies));
            containerBuilder.RegisterModule(new RepositoryModule(runtimeAssemblies));
            containerBuilder.RegisterModule(new CacheModule(runtimeAssemblies));
            containerBuilder.RegisterModule(new AutoMapperModule(runtimeAssemblies));
            containerBuilder.RegisterModule(new CustomMapperModule(runtimeAssemblies));
            containerBuilder.RegisterModule(new FluentValidationModule(runtimeAssemblies));

            containerBuilder.Populate(services);

            container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}
