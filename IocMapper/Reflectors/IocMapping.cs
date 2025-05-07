using System;

namespace IocMapper.Reflectors
{
    public class IocMapping
    {
        public Type Service { get; set; }

        public Type Implementation { get; set; }

        public Lifetimes Lifetime { get; set; }
    }
}
