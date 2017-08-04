using AbiokaRdn.Infrastructure.Common.Dynamic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AbiokaRdn.Infrastructure.Framework.Handlers
{
    public class ExceptionContext : IExceptionContext
    {
        public ExceptionContext(FilterContext context) {
            FilterContext = context;
        }

        public object Context => FilterContext;

        public FilterContext FilterContext { get; }

        public Exception Exception {
            get {
                if (FilterContext is Microsoft.AspNetCore.Mvc.Filters.ExceptionContext exceptionContext) {
                    return exceptionContext.Exception;
                }

                if (FilterContext is ActionExecutedContext actionExecutedContext) {
                    return actionExecutedContext.Exception;
                }

                return null;
            }
        }

        public void SetResult(IActionResult result) {
            if (FilterContext is Microsoft.AspNetCore.Mvc.Filters.ExceptionContext exceptionContext) {
                exceptionContext.Result = result;
                return;
            }

            if (FilterContext is ActionExecutedContext actionExecutedContext) {
                actionExecutedContext.Result = result;
            }
        }
    }
}
