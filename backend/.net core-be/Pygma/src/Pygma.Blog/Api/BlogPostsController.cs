using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.Api.Base;
using Pygma.Blog.Services.Abstractions;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Constants;
using Pygma.Common.Filters;
using Pygma.Common.Models.Search;
using Pygma.Data.Abstractions.Repositories;
using Pygma.Data.Domain.Entities;
using Pygma.Data.Domain.Enums;
using Pygma.Data.SearchCriteria;
using Pygma.Data.SearchSpecifications;
using Pygma.Services.Users;

namespace Pygma.Blog.Api
{
    [Route("api/blog-posts")]
    public class BlogPostsController : BlogControllerBase
    {
        private readonly IBlogPostsRepository _blogPostsRepository;
        private readonly IBlogPostsService _blogPostsService;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public BlogPostsController(IBlogPostsRepository blogPostsRepository,
            IBlogPostsService blogPostsService,
            IUsersService usersService,
            IMapper mapper)
        {
            _blogPostsRepository = blogPostsRepository;
            _blogPostsService = blogPostsService;
            _usersService = usersService;
            _mapper = mapper;
        }

        #region CRUD

        [HttpGet]
        [AllowAnonymous]
        [SkipInactiveUserFilter]
        public async Task<ActionResult<SearchResultsVm<BlogPostSrVm[]>>> SearchAsync(
            [FromQuery] SearchBlogPostVm searchBlogPostVm)
        {
            return await _blogPostsService.SearchAsync(_mapper.Map<BlogPostSc>(searchBlogPostVm));
        }

        [HttpGet("{id:int:min(1)}", Name = nameof(GetBlogPostAsync))]
        [AllowAnonymous]
        [SkipInactiveUserFilter]
        public async Task<ActionResult<BlogPostVm>> GetBlogPostAsync(int id)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            //Visitors can only view published blog posts
            if (_usersService.GetUser() == null
                && blogPost.Status != EnBlogPostStatus.Published)
            {
                return new ForbidResult();
            }

            return _mapper.Map<BlogPostVm>(blogPost);
        }

        [HttpPut("{id:int:min(1)}")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Author)]
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
        [Authorize(Roles = Roles.Admin + "," + Roles.Author)]
        public async Task<ActionResult<int>> CreateBlogPostAsync([FromBody] CreateBlogPostVm createBlogPostVm)
        {
            var blogPost = new BlogPost
            {
                AuthorId = _usersService.UserId
            };

            await _blogPostsRepository.CreateAsync(_mapper.Map(createBlogPostVm, blogPost));

            return Ok(blogPost.Id);
        }

        [HttpDelete("{id:int:min(1)}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> DeleteBlogPostAsync(int id)
        {
            var blogPost = await _blogPostsRepository.ReadByIdAsync(id);

            if (blogPost is null)
            {
                return NotFound();
            }

            await _blogPostsRepository.DeleteAsync(blogPost);

            return NoContent();
        }

        #endregion
    }
}