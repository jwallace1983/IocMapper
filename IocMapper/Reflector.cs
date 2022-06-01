using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IocMapper
{
    public static class Reflector
    {
        public static IEnumerable<Assembly> Assemblies => _assemblies;
        private static readonly List<Assembly> _assemblies = new List<Assembly>();

        public static void AddAssembly(Type type)
        {
            if (_assemblies.Any(a => a.FullName.Equals(type.Assembly.FullName)))
                return; // Guard: already added
            _assemblies.Add(type.Assembly);
        }
    }
}
