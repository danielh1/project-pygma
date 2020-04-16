using FluentValidation;

namespace Pygma.Common.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidateGeneralStringRequired<T>(
            this IRuleBuilder<T, string> ruleBuilder, int maxLength = 256)
        {
            return ruleBuilder
                .NotNull().WithMessage("Field is required")
                .NotEmpty().WithMessage("Field is required")                
                .MaximumLength(maxLength).WithMessage("Too many characters");
        }

        public static IRuleBuilderOptions<T, string> ValidateGeneralStringNullable<T>(
            this IRuleBuilder<T, string> ruleBuilder, int maxLength = 256)
        {
            return ruleBuilder
                .MaximumLength(maxLength)
                .When(x => !string.IsNullOrEmpty(x.ToString()))
                .WithMessage("Too many characters");
        }
    }
}
