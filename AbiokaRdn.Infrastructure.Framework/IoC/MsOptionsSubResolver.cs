using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Context;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace AbiokaRdn.Infrastructure.Framework.IoC
{
    public class MsOptionsSubResolver : ISubDependencyResolver
    {
        private readonly IKernel _kernel;

        public MsOptionsSubResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return dependency.TargetType.GetTypeInfo().IsGenericType && dependency.TargetType.GetGenericTypeDefinition() == typeof(IOptions<>);
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return _kernel.Resolve(dependency.TargetType);
        }
    }
}
