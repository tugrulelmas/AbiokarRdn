using System;

namespace AbiokaRdn.Infrastructure.Common.Exceptions.Adapters
{
    public interface IExceptionAdapterFactory
    {
        /// <summary>
        /// Gets the adapter.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        IExceptionAdapter GetAdapter(Exception exception);
    }
}
