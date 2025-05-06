using System;

namespace IocMapper.Mediator
{
    public class MediatorMappingException : Exception
    {
        public MediatorMappingException() : base("mapping-not-found") { }
    }
}
