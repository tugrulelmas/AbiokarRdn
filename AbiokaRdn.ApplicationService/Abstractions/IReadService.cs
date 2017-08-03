using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.Infrastructure.Common.Domain;
using System.Collections.Generic;

namespace AbiokaRdn.ApplicationService.Abstractions
{
    public interface IReadService<T> : IService where T : DTO
    {
        /// <summary>
        /// Gets the with page.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="order">The order.</param>
        /// <returns></returns>
        IPage<T> GetWithPage(int page, int limit, string order);

        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T Get(object id);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();
    }
}
