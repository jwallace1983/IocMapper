using System;

namespace IocMapper
{
    public class IocMappingException : Exception
    {
        public Type Implementation { get; }

        public IocMappingException() : base("mapping-error") { }

        public IocMappingException(Type implementation, string error = null) : base(error)
        {
            this.Implementation = implementation;
        }
    }
}
