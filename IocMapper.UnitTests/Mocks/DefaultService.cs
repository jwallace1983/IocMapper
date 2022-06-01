namespace IocMapper.UnitTests.Mocks
{
    public interface IDefaultService { }

    [Ioc]
    public class DefaultService : IDefaultService { }
}
