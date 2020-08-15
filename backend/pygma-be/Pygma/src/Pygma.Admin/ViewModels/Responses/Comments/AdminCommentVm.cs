using System;

namespace Pygma.Admin.ViewModels.Responses.Comments
{
    public class AdminCommentVm
    {
        public int Id { get; set; }
        public string VisitorName { get; set; }
        public string CommentText { get; set; }

        public int BlogPostId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}