using FluentValidation;
using Pygma.Blog.ViewModels.Requests.BlogPosts;

namespace Pygma.Blog.Validations.BlogPosts
{
    public class CreateBlogPostValidator : AbstractValidator<CreateBlogPostVm>
    {
        public CreateBlogPostValidator()
        {
            RuleFor(x => x)
                .SetValidator(new UpsertBlogPostValidator());
        }
    }
}