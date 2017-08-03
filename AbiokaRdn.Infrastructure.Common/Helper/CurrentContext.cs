using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.IoC;

namespace AbiokaRdn.Infrastructure.Common.Helper
{
    public class CurrentContext : ICurrentContext
    {
        public ICurrentContext Current => DependencyContainer.Container.Resolve<ICurrentContext>();

        public string IP { get; set; }

        public ICustomPrincipal Principal { get; set; }

        public ActionType ActionType { get; set; }
    }
}