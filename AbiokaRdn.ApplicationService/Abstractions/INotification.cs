using AbiokaRdn.ApplicationService.Messaging;

namespace AbiokaRdn.ApplicationService.Abstractions
{
    public interface INotificationService : IService
    {
        /// <summary>
        /// Sends the verification email.
        /// </summary>
        /// <param name="request">The request.</param>
        void SendVerificationEmail(SendVerificationEmailRequest request);
    }
}
