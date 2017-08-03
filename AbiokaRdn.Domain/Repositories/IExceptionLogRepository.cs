using AbiokaRdn.Infrastructure.Common.Domain;
using AbiokaRdn.Infrastructure.Common.Exceptions;

namespace AbiokaRdn.Domain.Repositories
{
    public interface IExceptionLogRepository : IReadOnlyRepository<ExceptionLog>
    {
        /// <summary>
        /// Adds the specified exception log.
        /// </summary>
        /// <param name="exceptionLog">The exception log.</param>
        void Add(ExceptionLog exceptionLog);
    }
}
