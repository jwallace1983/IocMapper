using System;

namespace IocMapper
{
    public class IocMapping
    {
        public Type Service { get; set; }

        public Type Implementation { get; set; }

        public Lifetimes Lifetime { get; set; }
    }
}
