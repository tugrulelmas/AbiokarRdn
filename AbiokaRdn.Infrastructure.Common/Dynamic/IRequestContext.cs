using System.Net.Http;

namespace AbiokaRdn.Infrastructure.Common.Dynamic
{
    public interface IRequestContext
    {
        /// <summary>
        /// Gets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        object Request { get; }
    }
}
