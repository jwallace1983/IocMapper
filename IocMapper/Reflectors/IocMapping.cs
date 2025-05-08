using System;

namespace IocMapper.Reflectors
{
    public class IocMapping(Type service, Type implementation, Lifetimes lifetime)
    {
        public Type Service { get; set; } = service;

        public Type Implementation { get; set; } = implementation;

        public Lifetimes Lifetime { get; set; } = lifetime;
    }
}
