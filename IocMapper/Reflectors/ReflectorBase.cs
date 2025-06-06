﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocMapper.Reflectors
{
    public abstract class ReflectorBase : IReflector
    {
        public IEnumerable<Assembly> Assemblies => _assemblies;

        private readonly List<Assembly> _assemblies = new List<Assembly>();
        
        public ReflectorBase(Type[] types)
        {
            foreach (var type in types)
                this.AddAssembly(type.Assembly);
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
