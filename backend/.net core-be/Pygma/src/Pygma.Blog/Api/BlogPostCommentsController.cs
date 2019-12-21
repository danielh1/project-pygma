using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;

namespace Pygma.Blog.Api
{
    [Route("api/blog-posts/{blogPostId:int:min(1)}/comments")]
    public class BlogPostCommentsController: BlogControllerBase
    {
        private readonly IBlogPostCommentsRepository _blogPostCommentsRepository;
        private readonly Mapper _mapper;

        public BlogPostCommentsController(IBlogPostCommentsRepository blogPostCommentsRepository,
            Mapper mapper)
        {
            _blogPostCommentsRepository = blogPostCommentsRepository;
            _mapper = mapper;
        }
        
        [HttpGet("{blogPostCommentId:int:min(1)}", Name = nameof(GetBlogPostCommentAsync))]
        public async Task<ActionResult<BlogPostCommentVm>> GetBlogPostCommentAsync(int blogPostId, int blogPostCommentId)
        {
            var blogPostComment = await _blogPostCommentsRepository.ReadByIdAndBlogPostIdAsync(blogPostCommentId, blogPostId);

            return blogPostComment == null 
                ? (ActionResult<BlogPostCommentVm>) NotFound($"Blog post comment {blogPostCommentId} not found") 
                : _mapper.Map<BlogPostCommentVm>(blogPostComment);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateBlogPostCommentAsync(int blogPostId,
            [FromBody] CreateBlogPostCommentVm createBlogPostCommentVm)
        {
            var blogPost = await _blogPostCommentsRepository.ReadByIdAsync(blogPostId);

            var blogPostComment = _mapper.Map<BlogPostComment>(createBlogPostCommentVm);
            blogPostComment.BlogPostId = blogPost.Id;
            
            return CreatedAtRoute(nameof(GetBlogPostCommentAsync),
                new {blogPostId = blogPost.Id, itemId = blogPostComment.Id}, null);
        }
    }
}