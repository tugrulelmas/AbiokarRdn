using AbiokaRdn.Infrastructure.Common.Dynamic;

namespace AbiokaRdn.Infrastructure.Common.Exceptions
{
    public interface IExceptionLogResolver
    {
        ExceptionLog Resolve(IExceptionContext exceptionContext);
    }
}
