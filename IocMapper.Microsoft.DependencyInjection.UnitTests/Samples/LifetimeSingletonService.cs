namespace IocMapper.Microsoft.DependencyInjection.UnitTests.Samples
{
    public interface ILifetimeSingletonService { int Counter { get; set; } }

    [Ioc(Lifetimes.Singleton)]
    public class LifetimeSingletonService : LifetimeServiceBase, ILifetimeSingletonService { }
}
