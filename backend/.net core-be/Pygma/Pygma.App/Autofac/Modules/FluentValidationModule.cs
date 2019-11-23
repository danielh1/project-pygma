using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Module = Autofac.Module;

namespace Pygma.App.Autofac.Modules
{
    public class FluentValidationModule: Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public FluentValidationModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assembly in _assemblies)
            {
                _ = builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Validator"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

//                builder.RegisterType<FluentValidationModelValidatorProvider>().As<ModelValidatorProvider>();
//
//                builder.RegisterType<AutofacValidatorFactory>().As<IValidatorFactory>().SingleInstance();

                base.Load(builder);
            }
        }
    }
}
