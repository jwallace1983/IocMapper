using System;

namespace IocMapper
{
    public class IocAttribute : Attribute
    {
        public Lifetimes Lifetime { get; }

        public Type? Target { get; set; }

        public IocAttribute() : this(Lifetimes.Transient) { }

        public IocAttribute(Lifetimes lifetime)
        {
            this.Lifetime = lifetime;
        }
    }
}
