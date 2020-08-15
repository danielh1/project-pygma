using AutoMapper;
using Pygma.Blog.ViewModels.Requests.Comments;
using Pygma.Blog.ViewModels.Responses.Comments;
using Pygma.Data.Domain.Entities;

namespace Pygma.Blog.Mapping.Auto.Profiles
{
    public class BlogPostCommentProfile: Profile
    {
        public BlogPostCommentProfile()
        {
            CreateMap<Comment, CommentVm>(MemberList.Destination);
            
            CreateMap<CreateCommentVm, Comment>(MemberList.Source);
        }
    }
}