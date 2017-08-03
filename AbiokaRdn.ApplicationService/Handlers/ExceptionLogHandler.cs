using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.ApplicationSettings;
using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.Exceptions;

namespace AbiokaRdn.ApplicationService.Handlers
{
    public class ExceptionLogHandler : IDynamicHandler
    {
        private readonly IExceptionLogResolver exceptionLogResolver;
        private readonly IExceptionLogRepository exceptionLogRepository;
        private readonly bool isExceptionLogEnabled = false;

        public ExceptionLogHandler(IExceptionLogResolver exceptionLogResolver, IExceptionLogRepository exceptionLogRepository, IConfigurationManager configurationManager) {
            this.exceptionLogResolver = exceptionLogResolver;
            this.exceptionLogRepository = exceptionLogRepository;

            isExceptionLogEnabled = configurationManager.ReadAppSetting("IsExceptionLogEnabled") == "true";
        }

        public short Order => 99;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
        }

        public void OnException(IExceptionContext exceptionContext) {
            if (!isExceptionLogEnabled)
                return;

            try {
                var exceptionLog = exceptionLogResolver.Resolve(exceptionContext);
                exceptionLogRepository.Add(exceptionLog);
            } catch {
                // TODO: log to file system.
            }
        }
    }
}
