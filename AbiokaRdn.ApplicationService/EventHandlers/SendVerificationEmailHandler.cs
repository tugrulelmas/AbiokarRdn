using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Domain;
using AbiokaRdn.Domain.Events;
using AbiokaRdn.Infrastructure.Common.Domain;

namespace AbiokaRdn.ApplicationService.EventHandlers
{
    public class SendVerificationEmailHandler : IEventHandler<UserIsAdded>
    {
        private readonly INotificationService notificationService;

        public SendVerificationEmailHandler(INotificationService notificationService) {
            this.notificationService = notificationService;
        }

        public int Order => 5;

        public void Handle(UserIsAdded eventInstance) {
            var user = eventInstance.User as UserSecurity;
            if (user == null || user.IsEmailVerified)
                return;

            notificationService.SendVerificationEmail(new SendVerificationEmailRequest { Email = user.Email });
        }
    }
}
