using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pygma.Admin.Api.Base;
using Pygma.Admin.ViewModels.Requests.Comments;
using Pygma.Admin.ViewModels.Responses.Comments;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;

namespace Pygma.Admin.Api
{
    [Route("api/admin/blog-posts/{id:int:min(1)}/comments")]
    public class AdminBlogPostCommentsController: AdminControllerBase
    {
        private readonly IBlogPostCommentsRepository _blogPostCommentsRepository;
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IMapper _mapper;
    
        public AdminBlogPostCommentsController(IBlogPostCommentsRepository blogPostCommentsRepository,
            IBlogPostsRepository blogPostsRepository,
            IMapper mapper)
        {
            _blogPostCommentsRepository = blogPostCommentsRepository;
            _blogPostsRepository = blogPostsRepository;
            _mapper = mapper;
        }
    
        #region CRUD
        
        [HttpGet("{commentId:int:min(1)}")]
        public async Task<ActionResult<AdminCommentVm>> GetAdminBlogPostCommentAsync(int id, int commentId)
        {
            var blogPostComment = await _blogPostCommentsRepository.ReadByIdAndBlogPostIdAsync(commentId, id);
    
            return blogPostComment == null 
                ? (ActionResult<AdminCommentVm>) NotFound($"Blog post comment {commentId} not found") 
                : _mapper.Map<AdminCommentVm>(blogPostComment);
        }

        
        [HttpPost]
        public async Task<ActionResult<int>> CreateAdminBlogPostCommentAsync(int id,
            [FromBody] CreateAdminCommentVm createCommentVm)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);
    
            var comment = _mapper.Map<Comment>(createCommentVm);
            comment.BlogPostId = blogPost.Id;
            
            await _blogPostCommentsRepository.CreateAsync(comment);

            return comment.Id;
        }
        
        [HttpDelete("{commentId:int:min(1)}")]
        public async Task<ActionResult> DeleteAdminBlogPostCommentAsync(int id, int commentId)
        {
            var comment = await _blogPostCommentsRepository.ReadByIdAsync(commentId);
    
            if (comment is null)
            {
                return NotFound();
            }
    
            await _blogPostCommentsRepository.DeleteAsync(comment);
    
            return NoContent();
        }
        #endregion
    }
}