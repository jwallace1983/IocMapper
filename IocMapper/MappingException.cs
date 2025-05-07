using System;

namespace IocMapper
{
    public class MappingException(Type type, string error = null)
        : Exception(error ?? "mapping-error")
    {
        public Type Type { get; } = type;
    }
}
