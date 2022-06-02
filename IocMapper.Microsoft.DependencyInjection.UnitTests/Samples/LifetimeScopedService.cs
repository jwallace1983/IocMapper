namespace IocMapper.Microsoft.DependencyInjection.UnitTests.Samples
{
    public interface ILifetimeScopedService { int Counter { get; set; } }

    [Ioc(Lifetimes.Scoped)]
    public class LifetimeScopedService : LifetimeServiceBase, ILifetimeScopedService { }
}
