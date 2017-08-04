using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.IoC;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace AbiokaRdn.Infrastructure.Framework.RestHelper.Attributes
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context) {
            var dynamicHandlers = DependencyContainer.Container.ResolveAll<IDynamicHandler>().OrderBy(d => d.Order);
            IExceptionContext exceptionContext = new Handlers.ExceptionContext(context);
            foreach (var dynamicHandlerItem in dynamicHandlers) {
                dynamicHandlerItem.OnException(exceptionContext);
            }
        }
    }
}