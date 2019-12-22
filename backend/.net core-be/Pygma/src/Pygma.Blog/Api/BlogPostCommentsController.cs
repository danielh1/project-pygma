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
    }
}