using AutoMapper;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Data.Domain.Entities;
using Pygma.Data.SearchCriteria;

namespace Pygma.Blog.Mapping.Auto.Profiles
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostVm>(MemberList.Destination);

            CreateMap<SearchBlogPostVm, BlogPostSc>(MemberList.Source);

            CreateMap<BlogPost, BlogPostSrVm>(MemberList.Destination)
                .ForMember(d => d.ShortDescription, opt => opt.MapFrom(s => s.Post.Substring(0, 200)));
        }
    }
}