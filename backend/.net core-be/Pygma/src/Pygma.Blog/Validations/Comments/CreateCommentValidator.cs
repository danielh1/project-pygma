using FluentValidation;
using Pygma.Blog.ViewModels.Requests.Comments;

namespace Pygma.Blog.Validations.Comments
{
    public class CreateCommentValidator: AbstractValidator<CreateCommentVm>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.VisitorName)
                .NotNull()
                .NotEmpty();
            
            RuleFor(x => x.CommentText)
                .NotNull()
                .NotEmpty();
        }
    }
}