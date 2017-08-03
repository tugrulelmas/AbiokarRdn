using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Infrastructure.Common.Authentication;
using System.Threading.Tasks;

namespace AbiokaRdn.ApplicationService.Authentication
{
    public interface IAuthService : IService
    {
        Task<string> LoginAsync(AuthRequest request);

        Task<string> RefreshTokenAsync(string refreshToken);

        AuthProvider Provider { get; }
    }
}
