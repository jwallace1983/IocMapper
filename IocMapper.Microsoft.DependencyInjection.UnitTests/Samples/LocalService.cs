namespace IocMapper.Microsoft.DependencyInjection.UnitTests.Samples
{
    public interface ILocalService
    {
        int Counter { get; set; }
    }

    [Ioc]
    public class LocalService : ILocalService
    {
        public int Counter { get; set; }
    }
}
