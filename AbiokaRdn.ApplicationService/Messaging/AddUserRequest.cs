using AbiokaRdn.ApplicationService.DTOs;
using AbiokaRdn.ApplicationService.Validation;
using AbiokaRdn.Domain.Repositories;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using FluentValidation;

namespace AbiokaRdn.ApplicationService.Messaging
{
    public class AddUserRequest : UserDTO
    {
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }
    }

    public class RegisterUserRequest : AddUserRequest
    {
    }

    public class AddUserRequestValidator : CustomValidator<AddUserRequest>
    {
        private readonly IUserSecurityRepository userSecurityRepository;

        public AddUserRequestValidator(IUserSecurityRepository userSecurityRepository) {
            this.userSecurityRepository = userSecurityRepository;

            RuleFor(r => r.Email).NotEmpty().WithMessage("IsRequired").EmailAddress().WithMessage("ShouldBeCorrectEmail");
            RuleFor(r => r.Password).NotEmpty().WithMessage("IsRequired");
        }

        protected override void DataValidate(AddUserRequest instance, ActionType actionType) {
            var tmpUser = userSecurityRepository.GetByEmail(instance.Email);
            if (tmpUser != null)
                throw new DenialException("UserIsAlreadyRegistered", instance.Email);
        }
    }
}
