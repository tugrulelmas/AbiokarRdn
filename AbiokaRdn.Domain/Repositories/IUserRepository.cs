using AbiokaRdn.Infrastructure.Common.Domain;

namespace AbiokaRdn.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Count of users.
        /// </summary>
        /// <returns></returns>
        int Count();
    }
}
