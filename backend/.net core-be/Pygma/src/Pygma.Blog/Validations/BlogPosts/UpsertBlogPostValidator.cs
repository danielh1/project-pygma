using FluentValidation;
using Pygma.Blog.ViewModels.Requests.Abstractions;
using Pygma.Common.Extensions;

namespace Pygma.Blog.Validations.BlogPosts
{
    public class UpsertBlogPostValidator : AbstractValidator<IUpsertBlogPost>
    {
        public UpsertBlogPostValidator()
        {
            RuleFor(x => x.Status).IsInEnum();
            
            RuleFor(x => x.Title)
                .ValidateGeneralStringRequired(50);
            
            RuleFor(x => x.Post)
                .NotNull()
                .NotEmpty();
        }
    }
}