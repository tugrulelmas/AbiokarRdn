using AbiokaRdn.Infrastructure.Common.Helper;
using FluentValidation.Results;

namespace AbiokaRdn.ApplicationService.Validation
{
    public interface ICustomValidator
    {
        ValidationResult Validate(object instance, ActionType actionType);
    }

    public interface ICustomValidator<in T>
    {
        ValidationResult Validate(T instance, ActionType actionType);
    }
}
