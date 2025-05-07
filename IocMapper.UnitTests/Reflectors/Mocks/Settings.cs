namespace IocMapper.UnitTests.Reflectors.Mocks
{
    [Ioc(Lifetimes.Singleton, Target = typeof(Settings))]
    public class Settings { }
}
