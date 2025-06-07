using System;

namespace IocMapper.Reflectors
{
    public class IocMapping
    {
        public Type Service { get; set; }

        public Type Implementation { get; set; }

        public Lifetimes Lifetime { get; set; }

        public IocMapping(Type service, Type implementation, Lifetimes lifetime)
        {
            this.Service = service;
            this.Implementation = implementation;
            this.Lifetime = lifetime;
        }
    }
}
