using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Blog.ViewModels.Requests.Comments;
using Pygma.Blog.ViewModels.Responses.Comments;
using Pygma.Common.Filters;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Domain.Enums;

namespace Pygma.Blog.Api
{
    [Route("api/blog-posts/{id:int:min(1)}/comments")]
    [AllowAnonymous]
    [SkipInactiveUserFilter]
    public class BlogPostCommentsController: BlogControllerBase
    {
        private readonly IBlogPostCommentsRepository _blogPostCommentsRepository;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IMapper _mapper;

        public BlogPostCommentsController(IBlogPostCommentsRepository blogPostCommentsRepository,
            IBlogPostsRepository blogPostsRepository,
            IMapper mapper)
        {
            _blogPostCommentsRepository = blogPostCommentsRepository;
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }

        [HttpGet("{commentId:int:min(1)}")]
        public async Task<ActionResult<CommentVm>> GetBlogPostCommentAsync(int id, int commentId)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);

            if (blogPost.Status != EnBlogPostStatus.Published)
            {
                return BadRequest();
            }
            
            var comment = await _blogPostCommentsRepository.ReadByIdAsync(commentId);

            if (comment is null)
            {
                return NotFound();
            }
            
            return _mapper.Map<CommentVm>(comment);
        }
        
        [HttpPost]
        public async Task<ActionResult<int>> CreateBlogPostCommentAsync(int id,
            [FromBody] CreateCommentVm createCommentVm)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);

            if (blogPost.Status != EnBlogPostStatus.Published)
            {
                return BadRequest();
            }
                
            var comment = _mapper.Map<Comment>(createCommentVm);
            comment.BlogPostId = blogPost.Id;
            
            await _blogPostCommentsRepository.CreateAsync(comment);
            
            return comment.Id;
        }
    }
}