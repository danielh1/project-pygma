using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Pygma.UatTests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RemoveRegisteredType(this IServiceCollection services, Type registeredType) 
        {
            var serviceDescriptors = services.Where(descriptor => descriptor.ServiceType == registeredType);
            foreach (var service in serviceDescriptors)
            {
                var t = services.Remove(service);
            }

            return services;
        }
    }
}