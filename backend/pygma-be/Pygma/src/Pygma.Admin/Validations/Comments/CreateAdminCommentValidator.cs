using FluentValidation;
using Pygma.Admin.ViewModels.Requests.Comments;

namespace Pygma.Admin.Validations.Comments
{
    public class CreateAdminCommentValidator: AbstractValidator<CreateAdminCommentVm>
    {
        public CreateAdminCommentValidator()
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