using FluentValidation;
using Pygma.Blog.ViewModels.Requests.Abstractions;

namespace Pygma.Blog.Validations.BlogPosts
{
    public class UpsertBlogPostValidator : AbstractValidator<IUpsertBlogPost>
    {
        public UpsertBlogPostValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.Post)
                .NotNull()
                .NotEmpty();
        }
    }
}