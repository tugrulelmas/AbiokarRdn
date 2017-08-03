using AbiokaRdn.ApplicationService.Messaging;
using System.Threading.Tasks;

namespace AbiokaRdn.ApplicationService.Abstractions
{
    public interface IEmailService : IService
    {
        /// <summary>
        /// Sends the asynchronous.
        /// </summary>
        /// <param name="emailRequest">The email request.</param>
        /// <returns></returns>
        Task SendAsync(EmailRequest emailRequest);
    }
}
