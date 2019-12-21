using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Models.Search;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications;

namespace Pygma.Blog.Api
{
    [Route("api/blog-posts")]
    public class BlogPostsController: BlogControllerBase
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogPostsRepository blogPostsRepository,
            IMapper mapper)
        {
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{blogPostId:int:min(1)}")]
        public async Task<ActionResult<BlogPostVm>> GetBlogPostAsync(int blogPostId)
        {
             var offer = await _blogPostsRepository.ReadByIdAsync(blogPostId);
             
             return _mapper.Map<BlogPostVm>(offer);
        }
        
        [HttpGet]
        public async Task<SearchResultsVm<BlogPostSrVm[]>> SearchAsync(BlogPostSc sc)
        {
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