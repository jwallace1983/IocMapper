namespace IocMapper.Microsoft.DependencyInjection.UnitTests.Samples
{
    public interface ILifetimeTransientService { int Counter { get; set; } }

    [Ioc(Lifetimes.Transient)]
    public class LifetimeTransientService : LifetimeServiceBase, ILifetimeTransientService { }
}
