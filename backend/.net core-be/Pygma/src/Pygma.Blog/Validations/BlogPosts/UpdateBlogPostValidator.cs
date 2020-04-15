using FluentValidation;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Common.Constants;
using Pygma.Data.Abstractions.Cache;
using Pygma.Services.Users;

namespace Pygma.Blog.Validations.BlogPosts
{
    public class UpdateBlogPostValidator : AbstractValidator<UpdateBlogPostVm>
    {
        public UpdateBlogPostValidator(IUsersService usersService, IUsersCache usersCache)
        {
            RuleFor(x => x)
                .SetValidator(new UpsertBlogPostValidator());
            
            if (usersService.GetPrincipalUser().IsInRole(Roles.Admin))
            {
                RuleFor(x => x)
                    .Custom((x, context) =>
                    {
                        if (usersCache.GetById(x.AuthorId) == null)
                        {
                            context.AddFailure("authorId", "Wrong Author Id");
                        }
                    });
                
                RuleFor(x => x.AuthorId)
                    .GreaterThan(0);
            }
        }
    }
}