using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Data.Domain.Enums;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Pygma.Users.ViewModels.Requests;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class BlogPostsTests : UatBase
    {
        private readonly HttpTestClient _http;

        public BlogPostsTests(HttpTestClient http)
        {
            _http = http;
        }

        [Fact]
        public async Task Get_DefaultClient_ShouldSucceed()
        {
            var blogPostVm = await _http
                .DefaultClient
                .GetBlogPostAsync(SeedConstants.PublishedBlogPost);

            blogPostVm.Id.Should().Be(SeedConstants.PublishedBlogPost);
            blogPostVm.BlogPostComments.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Get_DefaultClient_ForbidResult()
        {
            await _http
                .DefaultClient
                .GetBlogPostAsync(SeedConstants.InEditBlogPost, HttpStatusCode.Forbidden);
        }
        
        [Fact]
        public async Task Get_AuthorClient_InEditBlogPost_ShouldSucceed()
        {
            var actual = await _http
                .AuthorClient
                .GetBlogPostAsync(SeedConstants.InEditBlogPost);

            actual.Id.Should().Be(SeedConstants.InEditBlogPost);
        }
        
        [Fact]
        public async Task Create_ShouldSucceed()
        {
            var blogPostId = await _http
                .AuthorClient
                .CreateBlogPostAsync(new CreateBlogPostVm()
                {
                    Title = "Test Post",
                    Post = "Test content",
                    Status = EnBlogPostStatus.InEdit
                });

            blogPostId.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public async Task Update_ShouldSucceed()
        {
            var blogPostId = await _http
                .AuthorClient
                .CreateBlogPostAsync(new CreateBlogPostVm()
                {
                    Title = "Test Post",
                    Post = "Test content",
                    Status = EnBlogPostStatus.InEdit
                });

            blogPostId.Should().BeGreaterThan(0);
            
            await _http
                .AuthorClient
                .UpdateBlogPostAsync(blogPostId, new UpdateBlogPostVm()
                {
                    Title = "Test Post Updated",
                    Post = "Test content Updated",
                    Status = EnBlogPostStatus.Published,
                    AuthorId = SeedConstants.AuthorUser
                });
            
            var actual = await _http
                .AuthorClient
                .GetBlogPostAsync(blogPostId);

            actual.Id.Should().Be(blogPostId);
            actual.Title.Should().Be("Test Post Updated");
            actual.Post.Should().Be("Test content Updated");
            actual.Status.Should().Be(EnBlogPostStatus.Published);
        }
        
        [Fact]
        public async Task Update_BadRequest()
        {
            var blogPostId = await _http
                .AuthorClient
                .CreateBlogPostAsync(new CreateBlogPostVm()
                {
                    Title = "Test Post",
                    Post = "Test content",
                    Status = EnBlogPostStatus.InEdit
                });

            blogPostId.Should().BeGreaterThan(0);
            
            await _http
                .AdminClient
                .UpdateBlogPostAsync(blogPostId, new UpdateBlogPostVm()
                {
                    Title = "Test Post Updated",
                    Post = "Test content Updated",
                    Status = EnBlogPostStatus.Published,
                    AuthorId = 0 // -> This will throw validation error
                }, HttpStatusCode.BadRequest);
        }
        
        [Fact]
        public async Task Delete_ShouldSucceed()
        {
            var blogPostId = await _http
                .AuthorClient
                .CreateBlogPostAsync(new CreateBlogPostVm()
                {
                    Title = "Test Post",
                    Post = "Test content",
                    Status = EnBlogPostStatus.InEdit
                });

            blogPostId.Should().BeGreaterThan(0);

            await _http
                .AdminClient
                .DeleteBlogPostAsync(blogPostId);
            
            await _http
                .AuthorClient
                .GetBlogPostAsync(blogPostId, HttpStatusCode.NotFound);
        }
    }
}