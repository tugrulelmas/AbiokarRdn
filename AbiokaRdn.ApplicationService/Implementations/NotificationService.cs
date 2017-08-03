using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Domain;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Helper;
using System.Threading.Tasks;

namespace AbiokaRdn.ApplicationService.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailService emailService;
        private readonly ITemplateReader templateReader;
        private readonly IUserSecurityRepository userSecurityRepository;

        public NotificationService(IEmailService emailService, ITemplateReader templateReader, IUserSecurityRepository userSecurityRepository) {
            this.emailService = emailService;
            this.templateReader = templateReader;
            this.userSecurityRepository = userSecurityRepository;
        }

        public void SendVerificationEmail(SendVerificationEmailRequest request) {
            var user = userSecurityRepository.GetByEmail(request.Email);
            SendVerificationEmail(user);
        }

        private void SendVerificationEmail(UserSecurity user) {
            var template = templateReader.ReadTemplate(new ReadTemplateRequest {
                Key = "EmailVerifyTemplate",
                Language = user.Language
            });

            var bodyText = template.Body.Replace("{{Name}}", user.Name).Replace("{{Surname}}", user.Surname).Replace("{{Url}}", user.ProviderToken.EncodeWithBase64());

            var emailRequest = new EmailRequest {
                To = user.Email,
                Subject = template.Subject,
                Body = bodyText
            };

            Task.Run(async () => await emailService.SendAsync(emailRequest));
        }
    }
}
