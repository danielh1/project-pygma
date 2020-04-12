using FluentValidation;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Common.Validations;

namespace Pygma.Blog.Validations.BlogPosts
{
    public class SearchBlogPostValidator: AbstractValidator<SearchBlogPostVm>
    {
        public SearchBlogPostValidator()
        {
            RuleFor(x => x)
                .SetValidator(new PagingValidator());   
        }
    }
}