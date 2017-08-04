using System.Net.Http;

namespace AbiokaRdn.Infrastructure.Common.Dynamic
{
    public class RequestContext : IRequestContext
    {
        public RequestContext(object request) {
            Request = request;
        }

        public object Request { get; }
    }
}
