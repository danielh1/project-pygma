using FluentValidation;
using TrFoil.Backbone.Common.Abstractions;

namespace TrFoil.Backbone.Common.Validations
{
    public class PagingValidator : AbstractValidator<IPaging>
    {
        public PagingValidator()
        {
            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.CurrentPage)
                .InclusiveBetween(1, 1000);
        }
    }
}
