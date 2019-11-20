using System.Collections.Generic;
using System.Reflection;
using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace TrFoil.Backbone.App.Autofac.Modules
{
    public class AutoMapperModule : Module
    {
        private readonly IEnumerable<Assembly> _assemblies;

        public AutoMapperModule(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var assembly in _assemblies)
            {
                builder.RegisterAssemblyTypes(assembly).As<Profile>();

                builder.Register(context => new MapperConfiguration(cfg =>
                    {
                        foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                        {
                            cfg.AddProfile(profile);
                        }
                    }))
                    .AsSelf()
                    .SingleInstance();

                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.Name.EndsWith("Resolver"))
                    .AsSelf();

                builder.Register(c =>
                    {
                        //This resolves a new context that can be used later.
                        var context = c.Resolve<IComponentContext>();
                        var config = context.Resolve<MapperConfiguration>();
                        return config.CreateMapper(context.Resolve);
                    })
                    .As<IMapper>()
                    .SingleInstance();
            }
        }
    }
}