using System;

namespace IocMapper.Reflectors
{
    public class MediatorMapping
    {
        public Type HandlerType { get; }

        public Type RequestType { get; }

        public Type ResponseType { get; }

        public MediatorMapping(Type handlerType, Type requestType, Type responseType = null)
        {
            this.HandlerType = handlerType;
            this.RequestType = requestType;
            this.ResponseType = responseType;
        }
    }
}
