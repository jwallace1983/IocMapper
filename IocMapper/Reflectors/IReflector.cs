using Microsoft.Extensions.DependencyInjection;

namespace IocMapper.Reflectors
{
    public interface IReflector
    {
        void AddMappings(IServiceCollection services);
    }
}