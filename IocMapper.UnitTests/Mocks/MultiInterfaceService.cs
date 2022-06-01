namespace IocMapper.UnitTests.Mocks
{
    public interface IMultiService { }
    public interface IService1 { }
    public interface IService2 { }

    [Ioc(Target = typeof(IMultiService))]
    public class MultiInterfaceService : IService1, IService2, IMultiService { }
}
