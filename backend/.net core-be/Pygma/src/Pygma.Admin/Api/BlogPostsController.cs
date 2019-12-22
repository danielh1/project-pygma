using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pygma.Admin.Api.Base;
using Pygma.Admin.ViewModels.ViewModels.Requests.BlogPosts;
using Pygma.Admin.ViewModels.ViewModels.Responses.BlogPosts;
using Pygma.Common.Models.Search;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications;

namespace Pygma.Admin.Api
{
    [Route("api/admin/blog-posts")]
    public class BlogPostsController: AdminControllerBase
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogPostsRepository blogPostsRepository,
            IMapper mapper)
        {
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }
        
        #region CRUD
        [HttpPost]
        public async Task<ActionResult<int>> CreateBlogPostAsync([FromBody] CreateBlogPostVm createBlogPostVm)
        {
            var blogPost = new BlogPost();
            await _blogPostsRepository.CreateAsync(_mapper.Map(createBlogPostVm, blogPost));

            return Ok(blogPost.Id);
        }

        [HttpGet("{blogPostId:int:min(1)}", Name = nameof(GetBlogPostAsync))]
        public async Task<ActionResult<BlogPostVm>> GetBlogPostAsync(int blogPostId)
        {
             var offer = await _blogPostsRepository.ReadByIdAsync(blogPostId);
             
             return _mapper.Map<BlogPostVm>(offer);
        }

        [HttpPut("{blogPostId:int:min(1)}")]
        public async Task<ActionResult> UpdateBlogPostAsync(int blogPostId, UpdateBlogPostVm updateBlogPostVm)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(blogPostId);

            if (blogPost is null)
            {
                return NotFound();
            }

            await _blogPostsRepository.UpdateAsync(_mapper.Map(updateBlogPostVm, blogPost));

            return NoContent();
        }
        
        [HttpDelete("{blogPostId:int:min(1)}")]
        public async Task<ActionResult> DeleteBlogPostAsync(int blogPostId)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(blogPostId);

            if (blogPost is null)
            {
                return NotFound();
            }

            await _blogPostsRepository.DeleteAsync(blogPostId);

            return NoContent();
        }
        #endregion
        
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