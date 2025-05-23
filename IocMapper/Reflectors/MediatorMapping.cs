﻿using System;

namespace IocMapper.Reflectors
{
    public class MediatorMapping(Type handlerType, Type requestType, Type responseType = null)
    {
        public Type HandlerType { get; } = handlerType;

        public Type RequestType { get; } = requestType;

        public Type ResponseType { get; } = responseType;
    }
}
