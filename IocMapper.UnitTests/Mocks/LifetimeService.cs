namespace IocMapper.UnitTests.Mocks
{
    public interface ILifetimeService { }

    [Ioc(Lifetimes.Singleton)]
    public class LifetimeService : ILifetimeService { }
}
