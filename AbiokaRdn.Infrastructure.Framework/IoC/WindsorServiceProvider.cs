using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AbiokaRdn.Infrastructure.Framework.IoC
{
    public class WindsorServiceProvider : IServiceProvider
    {
        private readonly IWindsorContainer _container;

        public WindsorServiceProvider(IWindsorContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            // ASP.NET uses GetService<IEnumerable<TDesiredType>>() to get a collection.
            // This must be resolved to IWindsorContainer.ResolveAll();
            var typeInfo = serviceType.GetTypeInfo();
            if (typeInfo.IsGenericType && serviceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                var itemType = typeInfo.GenericTypeArguments[0];
                return _container.ResolveAll(itemType);
            }

            // Microsoft.Extensions.DependencyInjection is built to handle optional registrations.
            // However Castle Windsor throws ComponentNotFoundException when a type wasn't registered.
            // For this reason we have to manually check if the type exists in Windsor.
            if (_container.Kernel.HasComponent(serviceType))
            {
                return _container.Resolve(serviceType);
            }

            return null;
        }
    }
}
