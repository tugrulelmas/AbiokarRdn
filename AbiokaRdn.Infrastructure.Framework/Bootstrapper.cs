using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.IoC;
using AbiokaRdn.Infrastructure.Framework.Authentication;
using AbiokaRdn.Infrastructure.Framework.Handlers;
using AbiokaRdn.Infrastructure.Framework.IoC;
using AbiokaRdn.Infrastructure.Framework.RestHelper;

namespace AbiokaRdn.Infrastructure.Framework
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container
                .Register<ServiceInterceptor>(LifeStyle.Transient)
                .Register<IAbiokaToken, AbiokaToken>(isFallback: true)
                .Register<IDynamicHandler, AuthenticationHandler>(LifeStyle.PerWebRequest)
                .Register<IDynamicHandler, ExceptionHandler>()
                .Register<IExceptionLogResolver, ExceptionLogResolver>(isFallback: true);
        }
    }
}
