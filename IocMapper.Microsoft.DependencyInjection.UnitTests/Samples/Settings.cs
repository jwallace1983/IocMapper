namespace IocMapper.Microsoft.DependencyInjection.UnitTests.Samples
{
    [Ioc(Lifetimes.Singleton, Target = typeof(Settings))]
    public class Settings
    {
        public int Counter;
    }
}
