using System;
using System.Linq;
using System.Text;
using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Pygma.App.Autofac.Modules;
using Pygma.App.Extensions;

namespace Pygma.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string corsPolicy = "AllowMyOrigin";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddCors(options =>
            {
                options.AddPolicy(corsPolicy,
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                );
            });

            services.AddDbContext(Configuration);
            services.AddHttpContextAccessor();
            services.AddControllers(options => options.AddCoreFilters())
                .AddFluentValidation();
            services.AddSwagger();

            ConfigureAuth(services, Configuration);

            var runtimeAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.Contains("Pygma"))
                .ToList();

            services.AddAutoMapper(runtimeAssemblies);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(corsPolicy);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //var bootstrapCache = app.ApplicationServices.GetService<IBootstrapCacheService>();
            //bootstrapCache.Invalidate();

            app.UseHttpsRedirection();
            //app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var runtimeAssemblies = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x => x.FullName.Contains("Pygma"))
                .ToList();

            builder.RegisterModule(new ServiceModule(runtimeAssemblies));
            builder.RegisterModule(new RepositoryModule(runtimeAssemblies));
            builder.RegisterModule(new CacheModule(runtimeAssemblies));
            builder.RegisterModule(new CustomMapperModule(runtimeAssemblies));
            builder.RegisterModule(new FluentValidationModule(runtimeAssemblies));

            // builder.RegisterType<BootstrapCacheService>()
            //     .As<IBootstrapCacheService>()
            //     .SingleInstance();
            //
            // builder.RegisterType<UsersService>()
            //     .As<IUsersService>()
            //     .InstancePerLifetimeScope();
            //
            // builder.RegisterType<OrderViewAccessServiceFilter>()
            //     .InstancePerLifetimeScope();
            // builder.RegisterType<OrderEditAccessServiceFilter>()
            //     .InstancePerLifetimeScope();
        }

        public virtual void ConfigureAuth(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
        }
    }
}