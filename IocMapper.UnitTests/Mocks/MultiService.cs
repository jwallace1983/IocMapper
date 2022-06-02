namespace IocMapper.UnitTests.Mocks
{
    public interface IMultiService { }
    public interface IService1 { }
    public interface IService2 { }

    public abstract class MultiServiceBase { }

    [Ioc(Target = typeof(IMultiService))]
    public class MultiService : MultiServiceBase, IService1, IService2, IMultiService { }
}
