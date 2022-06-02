namespace IocMapper.UnitTests.Mocks
{
    [Ioc(Lifetimes.Singleton, Target = typeof(Settings))]
    public class Settings { }
}
