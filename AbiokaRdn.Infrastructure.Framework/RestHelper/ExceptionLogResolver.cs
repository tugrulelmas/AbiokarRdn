using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using AbiokaRdn.Infrastructure.Framework.Handlers;
using System;

namespace AbiokaRdn.Infrastructure.Framework.RestHelper
{
    public class ExceptionLogResolver : IExceptionLogResolver
    {
        private readonly ICurrentContext currentContext;

        public ExceptionLogResolver(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public ExceptionLog Resolve(IExceptionContext exceptionContext) {
            var context = (ExceptionContext)exceptionContext.Context;
            var errorCode = string.Empty;
            if(context.Exception is DenialException) {
                errorCode = ((ExceptionContent)((DenialException)context.Exception).ContentValue).ErrorCode;
            }

            return new ExceptionLog(context.Exception.Source, context.FilterContext.HttpContext.Request.ToString(), context.Exception.GetType().Name, errorCode, context.Exception.ToString(), 
                currentContext.Current.Principal?.Id ?? Guid.Empty,
                currentContext.Current.IP);
        }
    }
}
