using FluentValidation;
using Pygma.Common.Extensions;
using Pygma.Users.ViewModels.Requests.Account;

namespace Pygma.Users.Validations.Account
{
    public class LoginValidator: AbstractValidator<LoginVm>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .ValidateGeneralStringRequired(50);
            RuleFor(x => x.Password)
                .ValidateGeneralStringRequired(50);
        }
    }
}