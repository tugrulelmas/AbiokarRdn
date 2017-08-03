using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.ApplicationService.EventHandlers;
using AbiokaRdn.ApplicationService.Handlers;
using AbiokaRdn.ApplicationService.Implementations;
using AbiokaRdn.ApplicationService.Interceptors;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.ApplicationService.Validation;
using AbiokaRdn.Domain;
using AbiokaRdn.Infrastructure.Common.Domain;
using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.IoC;

namespace AbiokaRdn.ApplicationService
{
    public class Bootstrapper
    {
        public static void Initialise() {
            // TODO: uncomment
            //Repository.Bootstrapper.Initialise();
            DependencyContainer.Container
                .RegisterServices<IService, IService>()
                .RegisterService<ICrudService<RoleDTO>, CrudService<Role, RoleDTO>>()
                .RegisterService<IReadService<LoginAttemptDTO>, ReadService<LoginAttempt, LoginAttemptDTO>>()
                .RegisterWithBase(typeof(ICustomValidator<>), typeof(CustomValidator<>))
                .Register<ICustomValidator<RegisterUserRequest>, AddUserRequestValidator>()
                .RegisterWithBase(typeof(IEventHandler<>), typeof(RoleAddedToUserHandler))
                .Register<IServiceInterceptor, RoleValidationInterceptor>()
                .Register<IServiceInterceptor, DataValidationInterceptor>()
                .Register<IHttpClient, CustomHttpClient>(isFallback: true)
                .Register<IDTOMapper, DTOMapper>(isFallback: true)
                .Register<IDynamicHandler, ExceptionLogHandler>(isFallback: true);
        }
    }
}
