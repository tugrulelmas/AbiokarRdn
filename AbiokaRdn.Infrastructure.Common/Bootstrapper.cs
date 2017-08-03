using AbiokaRdn.Infrastructure.Common.Domain;
using AbiokaRdn.Infrastructure.Common.Exceptions.Adapters;
using AbiokaRdn.Infrastructure.Common.Helper;
using AbiokaRdn.Infrastructure.Common.IoC;

namespace AbiokaRdn.Infrastructure.Common
{
    public class Bootstrapper
    {
        public static void Initialise() {
            DependencyContainer.Container
                //.Register<IConfigurationManager, WebConfigManager>(isFallback: true)
                .Register<IExceptionAdapterFactory, ExceptionAdapterFactory>()
                .Register<ICurrentContext, CurrentContext>(LifeStyle.PerWebRequest, true)
                .Register<IEventDispatcher, EventDispatcher>(isFallback: true);
        }
    }
}
