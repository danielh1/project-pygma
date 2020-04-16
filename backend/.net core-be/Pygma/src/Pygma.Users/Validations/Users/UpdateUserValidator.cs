using FluentValidation;
using Pygma.Common.Extensions;
using Pygma.Users.ViewModels.Requests.Users;

namespace Pygma.Users.Validations.Users
{
    public class UpdateUserValidator: AbstractValidator<UpdateUserVm>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Firstname)
                .ValidateGeneralStringRequired(50);
            RuleFor(x => x.Lastname)
                .ValidateGeneralStringRequired(50);
        }
    }
}