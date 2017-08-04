﻿using AbiokaRdn.Infrastructure.Common.IoC;
using Castle.Core;
using Castle.MicroKernel.ModelBuilder.Descriptors;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using Castle.Facilities.TypedFactory;

namespace AbiokaRdn.Infrastructure.Framework.IoC
{
    public class CastleContainer : IDependencyContainer
    {
        public CastleContainer() {
            container = new WindsorContainer();
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));
            container.Kernel.AddFacility<TypedFactoryFacility>();
        }

        /// <summary>
        /// Resolve the target type with necessary dependencies.
        /// </summary>
        public object Resolve(Type targetType) {
            if (container.Kernel.HasComponent(targetType)) {
                return container.Resolve(targetType);
            }
            return null;
        }

        /// <summary>
        /// Resolves all registered instances for a specific service type.
        /// </summary>
        public IEnumerable<object> ResolveAll(Type serviceType) {
            if (container.Kernel.HasComponent(serviceType)) {
                return new List<object>((object[])container.ResolveAll(serviceType));
            }
            return null;
        }

        private readonly IWindsorContainer container;

        public IWindsorContainer Container => container;

        public T Resolve<T>() => container.Resolve<T>();

        public IEnumerable<T> ResolveAll<T>() => container.ResolveAll<T>();

        public IDependencyContainer Register<T>(LifeStyle lifeStyle, bool isFallback) => Register(typeof(T), lifeStyle, isFallback);

        public IDependencyContainer Register(Type type, LifeStyle lifeStyle, bool isFallback) {
            RegisterComponent(Component.For(type), lifeStyle, isFallback);
            return this;
        }

        public IDependencyContainer RegisterServices<T1, T2>() => RegisterServices(typeof(T1), typeof(T2));

        private IDependencyContainer RegisterServices(Type type1, Type type2) {
            container.Register(Classes.FromAssemblyContaining(type2).BasedOn(type1).WithService.FromInterface().Configure(c => c.LifestyleSingleton().Interceptors<ServiceInterceptor>()));
            return this;
        }

        public IDependencyContainer RegisterService<T1, T2>(LifeStyle lifeStyle) => RegisterService(typeof(T1), typeof(T2), lifeStyle);

        public IDependencyContainer RegisterService(Type type1, Type type2, LifeStyle lifeStyle) {
            RegisterComponent(Component.For(type1).ImplementedBy(type2).Interceptors<ServiceInterceptor>(), lifeStyle, false);
            return this;
        }

        public IDependencyContainer RegisterWithDefaultInterfaces<T1, T2>() => RegisterWithDefaultInterfaces(typeof(T1), typeof(T2));

        public IDependencyContainer RegisterWithDefaultInterfaces(Type type1, Type type2) {
            container.Register(Classes.FromAssemblyContaining(type2).BasedOn(type1).WithService.DefaultInterfaces().Configure(c => c.LifestyleSingleton().NamedAutomatically(Guid.NewGuid().ToString())));
            return this;
        }

        public IDependencyContainer RegisterWithBase<T1, T2>() => RegisterWithBase(typeof(T1), typeof(T2));

        public IDependencyContainer RegisterWithBase(Type type1, Type type2) {
            container.Register(Classes.FromAssemblyContaining(type2).BasedOn(type1).WithService.Base().Configure(c => c.LifestyleSingleton().NamedAutomatically(Guid.NewGuid().ToString())));
            return this;
        }

        public IDependencyContainer Register<T1, T2>(LifeStyle lifeStyle, bool isFallback) => Register(typeof(T1), typeof(T2), lifeStyle, isFallback);

        public IDependencyContainer Register(Type type1, Type type2, LifeStyle lifeStyle, bool isFallback) {
            RegisterComponent(Component.For(type1).ImplementedBy(type2), lifeStyle, isFallback);
            return this;
        }

        public void Release(object instance) {
            container.Release(instance);
        }

        public void Dispose() {
            container.Dispose();
        }

        public IDependencyContainer UsingFactoryMethod<T>(Func<T> func, bool isFallback) {
            RegisterComponent(Component.For(typeof(T)).UsingFactoryMethod(func), LifeStyle.Singleton, isFallback);
            return this;
        }

        public IDependencyContainer RegisterAsFactory<T>() where  T : class {
            RegisterComponent(Component.For<T>().AsFactory(), LifeStyle.Singleton, false);
            return this;
        }

        private void RegisterComponent<T>(ComponentRegistration<T> componentRegistration, LifeStyle lifeStyle, bool isFallback) where T : class {
            var lifestyleDescriptor = new LifestyleDescriptor<T>(GetLifestyleType(lifeStyle));
            componentRegistration.AddDescriptor(lifestyleDescriptor);

            if (isFallback) {
                componentRegistration = componentRegistration.IsFallback().NamedAutomatically(Guid.NewGuid().ToString());
            }

            container.Register(componentRegistration);
        }

        private LifestyleType GetLifestyleType(LifeStyle lifeStyle) {
            switch (lifeStyle) {
                case LifeStyle.PerThread:
                    return LifestyleType.Thread;
                case LifeStyle.PerWebRequest:
                    return LifestyleType.Scoped;
                case LifeStyle.Singleton:
                    return LifestyleType.Singleton;
                case LifeStyle.Transient:
                    return LifestyleType.Transient;
                default:
                    throw new NotSupportedException($"{lifeStyle} is not a supported life style.");
            }
        }
    }
}
