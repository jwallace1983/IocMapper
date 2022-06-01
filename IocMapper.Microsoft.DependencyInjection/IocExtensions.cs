using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IocExtensions
    {
        public static IServiceCollection AddIocMappings(this IServiceCollection services, params Type[] sources)
        {
            return services;
        }
    }
}
