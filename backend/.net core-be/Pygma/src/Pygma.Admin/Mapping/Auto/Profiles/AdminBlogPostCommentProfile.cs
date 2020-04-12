using AutoMapper;
using Pygma.Admin.ViewModels.Requests.Comments;
using Pygma.Admin.ViewModels.Responses.Comments;
using Pygma.Data.Domain.Entities;

namespace Pygma.Admin.Mapping.Auto.Profiles
{
    public class AdminBlogPostCommentProfile: Profile
    {
        public AdminBlogPostCommentProfile()
        {
            CreateMap<Comment, AdminCommentVm>(MemberList.Destination);
            
            CreateMap<CreateAdminCommentVm, Comment>(MemberList.Source);
        }
    }
}