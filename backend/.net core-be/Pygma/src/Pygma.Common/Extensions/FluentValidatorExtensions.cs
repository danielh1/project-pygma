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

        // public static IRuleBuilderOptions<T, decimal> ValidateMaxCabinetHeight<T>(
        //     this IRuleBuilder<T, decimal> ruleBuilder)
        // {
        //     return ruleBuilder
        //         .LessThan(Max.CabinetHeight)
        //         .WithMessage("Max Cabinet height exceeded")
        //         .GreaterThan(0)
        //         .WithMessage("Invalid Cabinet height");
        // }
        //
        // public static IRuleBuilderOptions<T, decimal> ValidateMaxCabinetWidth<T>(
        //     this IRuleBuilder<T, decimal> ruleBuilder)
        // {
        //     return ruleBuilder                
        //         .LessThan(Max.CabinetWidth)
        //         .WithMessage("Max Cabinet width exceeded")
        //         .GreaterThan(0)
        //         .WithMessage("Invalid Cabinet width");
        // }
        //
        // public static IRuleBuilderOptions<T, decimal> ValidateMaxPageSize<T>(
        //     this IRuleBuilder<T, decimal> ruleBuilder)
        // {
        //     return ruleBuilder                
        //         .LessThanOrEqualTo(Max.PageSize)
        //         .WithMessage("Max PageSize exceeded")
        //         .GreaterThan(0)
        //         .WithMessage("Invalid PageSize");
        // }
    }
}
