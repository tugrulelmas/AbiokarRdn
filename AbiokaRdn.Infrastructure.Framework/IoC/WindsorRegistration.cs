using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace AbiokaRdn.Infrastructure.Framework.IoC
{
    public static class WindsorRegistration
    {
        public static void Populate(this IWindsorContainer container,
            IEnumerable<Microsoft.Extensions.DependencyInjection.ServiceDescriptor> descriptors)
        {
            container.Register(Component.For<IWindsorContainer>().Instance(container).OnlyNewServices());

            container.Register(Component.For<IServiceProvider>().ImplementedBy<WindsorServiceProvider>());
            container.Register(Component.For<IServiceScopeFactory>().ImplementedBy<WindsorServiceScopeFactory>());

            // ASP.NET Core uses IEnumerable<T> to resolve a list of types.
            // Since some of these types are optional, Windsor must also return empty collections.
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, allowEmptyCollections: true));

            container.Kernel.Resolver.AddSubResolver(new MsOptionsSubResolver(container.Kernel));

            Register(container, descriptors);
        }



        private static void Register(IWindsorContainer container,
            IEnumerable<Microsoft.Extensions.DependencyInjection.ServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                if (descriptor.ImplementationType != null)
                {
                    container.Register(
                            Component.For(descriptor.ServiceType)
                                 .Named(Guid.NewGuid().ToString())
                                .ImplementedBy(descriptor.ImplementationType)
                                .ConfigureLifecycle(descriptor.Lifetime));
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    var service1 = descriptor;
                    container.Register(
                        Component.For(descriptor.ServiceType)
                            .UsingFactoryMethod(c =>
                            {
                                var serviceProvider = container.Resolve<IServiceProvider>();
                                return service1.ImplementationFactory(serviceProvider);
                            })
                            .ConfigureLifecycle(descriptor.Lifetime));
                }
                else
                {
                    container.Register(
                        Component.For(descriptor.ServiceType)
                            .Named(Guid.NewGuid().ToString())
                            .Instance(descriptor.ImplementationInstance)
                            .ConfigureLifecycle(descriptor.Lifetime));
                }
            }
        }

        private static ComponentRegistration<object> ConfigureLifecycle(
            this ComponentRegistration<object> registrationBuilder,
            ServiceLifetime serviceLifetime)
        {
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    registrationBuilder.LifestyleSingleton();
                    break;

                case ServiceLifetime.Scoped:
                    registrationBuilder.LifestyleScoped();
                    break;

                case ServiceLifetime.Transient:
                    registrationBuilder.LifestyleTransient();
                    break;
            }

            return registrationBuilder;
        }
    }
}
