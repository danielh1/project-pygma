using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Constants;
using Pygma.Common.Models.Search;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
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
        
        #region CRUD
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

        [HttpGet("{id:int:min(1)}", Name = nameof(GetBlogPostAsync))]
        public async Task<ActionResult<BlogPostVm>> GetBlogPostAsync(int id)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);
             
            if (blogPost is null)
            {
                return NotFound();
            }
             
            return _mapper.Map<BlogPostVm>(blogPost);
        }
        
        [HttpPut("{id:int:min(1)}")]
        [Authorize(Roles = Roles.Author)]
        public async Task<ActionResult> UpdateBlogPostAsync(int id, UpdateBlogPostVm updateBlogPostVm)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            await _blogPostsRepository.UpdateAsync(_mapper.Map(updateBlogPostVm, blogPost));

            return NoContent();
        }
        
        [HttpPost]
        [Authorize(Roles = Roles.Author)]
        public async Task<ActionResult<int>> CreateBlogPostAsync([FromBody] CreateBlogPostVm createBlogPostVm)
        {
            var blogPost = new BlogPost();
            await _blogPostsRepository.CreateAsync(_mapper.Map(createBlogPostVm, blogPost));

            return Ok(blogPost.Id);
        }
        
        [HttpDelete("{blogPostId:int:min(1)}")]
        [Authorize(Roles = Roles.Author)]
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
    }
}