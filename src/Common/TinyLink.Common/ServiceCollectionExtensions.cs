using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using TinyLink.Common;
using TinyLink.Common.Dependency;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, Action<TinyLinkCommonOptions> action) 
        {
            var options = new TinyLinkCommonOptions();
            action?.Invoke(options);
            services.AddSingleton(options);

            var assemblies = LoadAssemblies();

            services.AddWithSingletonLifetime(assemblies, new[] { typeof(ISingletonLifetime) });
            services.AddWithScopedLifetime(assemblies, new[] { typeof(IScopedLifetime) });
            services.AddWithTransientLifetime(assemblies, new[] { typeof(ITransientLifetime) });

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(assemblies);
                options.AutomaticValidationEnabled = false;
            });

            return services;
        }

        private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
        {
            services.Scan(source => source.FromAssemblies(assemblies)
                .AddClasses(type => type.AssignableToAny(types))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }

        private static IServiceCollection AddWithScopedLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
        {
            services.Scan(source => source.FromAssemblies(assemblies)
                .AddClasses(type => type.AssignableToAny(types))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddWithSingletonLifetime(this IServiceCollection services, IEnumerable<Assembly> assemblies, IEnumerable<Type> types)
        {
            services.Scan(source => source.FromAssemblies(assemblies)
                .AddClasses(type => type.AssignableToAny(types))
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

            return services;
        }

        private static Assembly[] LoadAssemblies(params string[] assemblyNames)
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var dependency in dependencies)
            {
                if (assemblyNames.Any(x => x.Contains(dependency.Name, StringComparison.OrdinalIgnoreCase)))
                {
                    var assembly = Assembly.Load(new AssemblyName(dependency.Name));
                    assemblies.Add(assembly);
                }
            }
            return assemblies.ToArray();
        }
    }
}
