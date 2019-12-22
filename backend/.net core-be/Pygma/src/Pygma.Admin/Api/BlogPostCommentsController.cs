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
    [Route("api/blog-posts/{blogPostId:int:min(1)}/comments")]
    public class BlogPostCommentsController: AdminControllerBase
    {
        private readonly IBlogPostCommentsRepository _blogPostCommentsRepository;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly Mapper _mapper;

        public BlogPostCommentsController(IBlogPostCommentsRepository blogPostCommentsRepository,
            IBlogPostsRepository blogPostsRepository,
            Mapper mapper)
        {
            _blogPostCommentsRepository = blogPostCommentsRepository;
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }

        #region CRUD
        [HttpPost]
        public async Task<ActionResult> CreateBlogPostCommentAsync(int blogPostId,
            [FromBody] CreateBlogPostCommentVm createBlogPostCommentVm)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(blogPostId);

            var blogPostComment = _mapper.Map<BlogPostComment>(createBlogPostCommentVm);
            blogPostComment.BlogPostId = blogPost.Id;
            
            await _blogPostCommentsRepository.CreateAsync(blogPostComment);
            
            return CreatedAtRoute(nameof(GetBlogPostCommentAsync),
                new {blogPostId = blogPost.Id, itemId = blogPostComment.Id}, null);
        }
        
        [HttpGet("{blogPostCommentId:int:min(1)}", Name = nameof(GetBlogPostCommentAsync))]
        public async Task<ActionResult<BlogPostCommentVm>> GetBlogPostCommentAsync(int blogPostId, int blogPostCommentId)
        {
            var blogPostComment = await _blogPostCommentsRepository.ReadByIdAndBlogPostIdAsync(blogPostCommentId, blogPostId);

            return blogPostComment == null 
                ? (ActionResult<BlogPostCommentVm>) NotFound($"Blog post comment {blogPostCommentId} not found") 
                : _mapper.Map<BlogPostCommentVm>(blogPostComment);
        }
        
        [HttpPut("{blogPostId:int:min(1)}")]
        public async Task<ActionResult> UpdateBlogPostCommentAsync(int blogPostCommentId, UpdateBlogPostCommentVm updateBlogPostCommentVm)
        {
            var blogPostComment = await _blogPostCommentsRepository.ReadByIdAsync(blogPostCommentId);

            if (blogPostComment is null)
            {
                return NotFound();
            }

            await _blogPostCommentsRepository.UpdateAsync(_mapper.Map(updateBlogPostCommentVm, blogPostComment));

            return NoContent();
        }
        
        [HttpDelete("{blogPostCommentId:int:min(1)}")]
        public async Task<ActionResult> DeleteBlogPostCommentAsync(int blogPostCommentId)
        {
            var blogPostComment = await _blogPostCommentsRepository.ReadByIdAsync(blogPostCommentId);

            if (blogPostComment is null)
            {
                return NotFound();
            }

            await _blogPostsRepository.DeleteAsync(blogPostCommentId);

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