using AbiokaRdn.ApplicationService.Validation;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using FluentValidation;

namespace AbiokaRdn.ApplicationService.Messaging
{
    public class CreateApplicationDataRequest : ServiceRequestBase
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }

    public class CreateApplicationDataRequestValidator : CustomValidator<CreateApplicationDataRequest>
    {
        private readonly IUserRepository userRepository;

        public CreateApplicationDataRequestValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(CreateApplicationDataRequest instance, ActionType actionType) {
            var count = userRepository.Count();
            if (count > 0)
                throw new DenialException("DBIsNotEmpty");
        }
    }
}
