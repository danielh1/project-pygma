using AutoMapper;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Extensions;
using Pygma.Data.Domain.Entities;
using Pygma.Data.SearchCriteria;

namespace Pygma.Blog.Mapping.Auto.Profiles
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPost, BlogPostVm>(MemberList.Destination);

            CreateMap<SearchBlogPostVm, BlogPostSc>(MemberList.Destination)
                .ForMember(d => d.OrderBy, opt => opt.MapFrom(s => s.OrderBy.ToString().CreateExpression<BlogPost>()));

            CreateMap<BlogPost, BlogPostSrVm>(MemberList.Destination)
                .ForMember(d => d.ShortDescription,
                    opt => opt.MapFrom(s => s.Post.Substring(0, s.Post.Length > 200 ? 200 : s.Post.Length)))
                .ForMember(d => d.AuthorLastname, opt => opt.MapFrom(s => s.Author.LastName));

            CreateMap<CreateBlogPostVm, BlogPost>(MemberList.Source);
            CreateMap<UpdateBlogPostVm, BlogPost>(MemberList.Source);
        }
    }
}