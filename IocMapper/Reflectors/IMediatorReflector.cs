using System;
using System.Collections.Generic;

namespace IocMapper.Reflectors
{
    public interface IMediatorReflector : IReflector
    {
        Dictionary<string, MediatorMapping> Mappings { get; }

        string GetMediatorMappingKey(Type type);
    }
}