using AutoMapper;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Data.Domain.Entities;

namespace Pygma.Blog.Mapping.Auto.Profiles
{
    public class BlogPostCommentProfile: Profile
    {
        public BlogPostCommentProfile()
        {
            CreateMap<CreateBlogPostCommentVm, BlogPostComment>(MemberList.Source);
        }
    }
}