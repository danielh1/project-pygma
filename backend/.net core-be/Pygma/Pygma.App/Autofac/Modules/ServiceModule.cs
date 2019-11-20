using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace TrFoil.Backbone.App.Autofac.Modules
{
    public class ServiceModule : Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public ServiceModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assembly in _assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();
            }
        }
    }
}
