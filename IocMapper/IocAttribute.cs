using System;

namespace IocMapper
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class IocAttribute(Lifetimes lifetime) : Attribute
    {
        public Lifetimes Lifetime { get; } = lifetime;

        public Type Target { get; set; }

        public IocAttribute() : this(Lifetimes.Transient) { }
    }
}
