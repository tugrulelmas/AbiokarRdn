using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.Exceptions.Adapters;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace AbiokaRdn.Infrastructure.Framework.Handlers
{
    public class ExceptionHandler : IDynamicHandler
    {
        private readonly IExceptionAdapterFactory exceptionAdapterFactory;

        public ExceptionHandler(IExceptionAdapterFactory exceptionAdapterFactory) {
            this.exceptionAdapterFactory = exceptionAdapterFactory;
        }

        public short Order => 100;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
        }

        public void OnException(IExceptionContext exceptionContext) {
            var context =  (ExceptionContext)exceptionContext;
            IExceptionAdapter exceptionAdapter = exceptionAdapterFactory.GetAdapter(context.Exception);

            context.FilterContext.HttpContext.Response.StatusCode = (int)exceptionAdapter.HttpStatusCode;

            var response = new HttpResponseMessage(exceptionAdapter.HttpStatusCode);

            if (exceptionAdapter.ExtraHeaders != null) {
                foreach (var headerItem in exceptionAdapter.ExtraHeaders) {
                    context.FilterContext.HttpContext.Response.Headers.Add(headerItem.Key, headerItem.Value);
                }
            }
            context.SetResult(new JsonResult(exceptionAdapter.Content));
        }
    }
}
