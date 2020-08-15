using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Pygma.App.Autofac.Modules
{
    public class CustomMapperModule : Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public CustomMapperModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assembly in _assemblies)
            {
                _ = builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Mapper"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

                //When using the classes directly
                //                builder.RegisterAssemblyTypes(assembly)
                //                    .Where(x => x.IsSubclassOf(typeof(MapperBase)))
                //                    .InstancePerLifetimeScope();

                //                builder.RegisterAssemblyTypes(
                //                        AppDomain.CurrentDomain.GetAssemblies())
                //                    .AsClosedTypesOf(typeof(ICommandHandler<>));
            }
        }
    }
}
