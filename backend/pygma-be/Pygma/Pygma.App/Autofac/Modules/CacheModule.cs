using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Pygma.App.Autofac.Modules
{
    public class CacheModule : Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public CacheModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assembly in _assemblies)
            {
                _ = builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Cache"))
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
    }
}
