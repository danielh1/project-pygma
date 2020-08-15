using System.Threading.Tasks;
using AutoMapper;
using Pygma.Blog.Services.Abstractions;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Models.Search;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Enums;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications;
using Pygma.Services.Users;

namespace Pygma.Blog.Services
{
    public class BlogPostsService: IBlogPostsService
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public BlogPostsService(
            IBlogPostsRepository blogPostsRepository,
            IUsersService usersService,
            IMapper mapper)
        {
            _blogPostsRepository = blogPostsRepository;
            _usersService = usersService;
            _mapper = mapper;
        }
        
        public async Task<SearchResultsVm<BlogPostSrVm[]>> SearchBlogPostsAsync(BlogPostSc sc)
        {
            //Visitors can only view published blog posts
            if (_usersService.GetUser() == null)
            {
                sc.Status = EnBlogPostStatus.Published;
            }
            
            var specification = new BlogPostSpecification();
            specification.SetCriteria(sc);

            var itemsOnPage = await _blogPostsRepository.SearchAsync(specification);
            var totalItems = await _blogPostsRepository.CountAsync(specification);

            var results = new SearchResultsVm<BlogPostSrVm[]>(
                _mapper.Map<BlogPostSrVm[]>(itemsOnPage),
                totalItems,
                itemsOnPage.Count,
                sc.CurrentPage,
                sc.Take);

            return results;
        }
    }
}