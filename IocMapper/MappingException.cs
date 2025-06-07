using System;

namespace IocMapper
{
    public class MappingException : Exception
    {
        public Type Type { get; }

        public MappingException(Type type, string error = null) : base(error ?? "mapping-error")
            => this.Type = type;
    }
}
