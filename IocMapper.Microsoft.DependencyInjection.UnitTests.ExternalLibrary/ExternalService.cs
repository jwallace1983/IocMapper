namespace IocMapper.Microsoft.DependencyInjection.UnitTests.ExternalLibrary
{
    public interface IExternalService { }

    [Ioc]
    public class ExternalService : IExternalService { }
}