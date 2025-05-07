namespace IocMapper.UnitTests.Reflectors.Mocks
{
    public interface ILifetimeService { }

    [Ioc(Lifetimes.Singleton)]
    public class LifetimeService : ILifetimeService { }
}
