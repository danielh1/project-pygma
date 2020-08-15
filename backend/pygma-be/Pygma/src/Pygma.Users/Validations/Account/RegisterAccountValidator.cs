using FluentValidation;
using Pygma.Common.Extensions;
using Pygma.Users.ViewModels.Requests.Account;

namespace Pygma.Users.Validations.Account
{
    public class RegisterAccountValidator: AbstractValidator<RegisterAccountVm>
    {
        public RegisterAccountValidator()
        {
            RuleFor(x => x.Firstname)
                .ValidateGeneralStringRequired(50);
            RuleFor(x => x.Lastname)
                .ValidateGeneralStringRequired(50);
            RuleFor(x => x.Email)
                .EmailAddress()
                .ValidateGeneralStringRequired(50);
            RuleFor(x => x.Password)
                .ValidateGeneralStringRequired(50);
        }
    }
}