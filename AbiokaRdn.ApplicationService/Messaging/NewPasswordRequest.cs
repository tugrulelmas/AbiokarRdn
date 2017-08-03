using AbiokaRdn.ApplicationService.Validation;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using FluentValidation;
using System.Linq;
using System.Net;

namespace AbiokaRdn.ApplicationService.Messaging
{
    public class NewPasswordRequest : ServiceRequestBase
    {
        public string Token { get; set; }

        public string Password { get; set; }
    }

    public class NewPasswordRequestdRequestValidator : CustomValidator<NewPasswordRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;

        public NewPasswordRequestdRequestValidator(IUserSecurityRepository userSecurityRepository) {
            this.userSecurityRepository = userSecurityRepository;

            RuleFor(cp => cp.Password).NotEmpty().WithMessage("IsRequired");
            RuleFor(cp => cp.Token).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(NewPasswordRequest instance, ActionType actionType) {
            var providerToken = instance.Token.DecodeBase64();
            var user = userSecurityRepository.Query().Where(u => u.ProviderToken == providerToken).FirstOrDefault();
            if (user == null)
                throw new DenialException(HttpStatusCode.NotFound, "UserNotFound");

            if (user.AuthProvider != AuthProvider.Local)
                throw new DenialException("PasswordCannotBeChanged");

            if (!user.IsEmailVerified)
                throw new DenialException(HttpStatusCode.NotFound, "PleaseVerifyYourAccount");
        }
    }
}
