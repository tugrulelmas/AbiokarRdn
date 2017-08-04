using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AbiokaRdn.Infrastructure.Framework.IoC
{
    public class WindsorServiceScope : IServiceScope
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IDisposable _scope;

        public WindsorServiceScope(IWindsorContainer container)
        {
            _scope = container.BeginScope();
            _serviceProvider = container.Resolve<IServiceProvider>();
        }

        public IServiceProvider ServiceProvider => _serviceProvider;

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
