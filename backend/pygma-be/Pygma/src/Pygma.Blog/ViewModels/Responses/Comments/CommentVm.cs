namespace Pygma.Blog.ViewModels.Responses.Comments
{
    public class CommentVm
    {
        public int Id { get; set; }
        public string VisitorName { get; set; }
        public string CommentText { get; set; }

        public int BlogPostId { get; set; }
    }
}