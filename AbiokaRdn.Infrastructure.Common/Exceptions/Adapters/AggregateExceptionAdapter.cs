using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AbiokaRdn.Infrastructure.Common.Exceptions.Adapters
{
    public class AggregateExceptionAdapter : IExceptionAdapter
    {
        private AggregateException exception;

        public AggregateExceptionAdapter(AggregateException exception) {
            this.exception = exception;
        }

        public object Content {
            get {
                var exceptionMessage = new StringBuilder();
                foreach (var exceptionItem in exception.InnerExceptions) {
                    exceptionMessage.AppendLine(exceptionItem.ToString());
                }
                return exceptionMessage.ToString();
            }
        }

        public IDictionary<string, string> ExtraHeaders => null;

        public HttpStatusCode HttpStatusCode => HttpStatusCode.InternalServerError;
    }
}