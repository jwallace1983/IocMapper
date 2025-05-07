using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocMapper.Reflectors
{
    public abstract class ReflectorBase : IReflector
    {
        public IEnumerable<Assembly> Assemblies => _assemblies;

        private readonly List<Assembly> _assemblies = [];
        
        public ReflectorBase(Type[] types)
        {
            if (types.Length == 0)
                this.AddAssembly(Assembly.GetCallingAssembly());
            foreach (var type in types)
                this.AddAssembly(type.Assembly);
        }

        public ReflectorBase(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
                this.AddAssembly(assembly);
        }

        public void AddAssembly(Assembly assembly)
        {
            if (_assemblies.Any(a => a.FullName.Equals(assembly.FullName)))
                return; // Guard: already added
            _assemblies.Add(assembly);
        }

        public void AddMappings(IServiceCollection services)
        {
            foreach (var assembly in _assemblies)
                foreach (var type in assembly.GetTypes())
                    this.AddMappingFromType(services, type);
        }

        public abstract void AddMappingFromType(IServiceCollection services, Type type);

    }
}
