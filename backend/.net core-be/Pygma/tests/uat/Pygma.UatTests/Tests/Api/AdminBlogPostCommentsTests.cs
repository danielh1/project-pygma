using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Pygma.Blog.ViewModels.Requests.BlogPostComments;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class AdminBlogPostCommentsTests : UatBase
    {
        private readonly HttpTestClient _http;
        
        public AdminBlogPostCommentsTests(HttpTestClient http)
        {
            _http = http;
        }
        
        [Fact]
        public async Task Get_ShouldSucceed()
        {
            var comment = await _http
                .AdminClient
                .GetAdminCommentAsync(SeedConstants.PublishedBlogPost, SeedConstants.PublishedComment1);

            comment.Should().NotBeNull();
            comment.Id.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public async Task Create_ShouldSucceed()
        {
            var commentId = await _http
                .AdminClient
                .CreateAdminCommentAsync(SeedConstants.PublishedBlogPost,
                    new CreateCommentVm()
                    {
                        VisitorName = "Admin Test visitor",
                        CommentText = "Admin Test comment"
                    });

            commentId.Should().BeGreaterThan(0);
            
            var comment = await _http
                .AdminClient
                .GetAdminCommentAsync(SeedConstants.PublishedBlogPost, commentId);

            comment.Should().NotBeNull();
            comment.Id.Should().Be(commentId);
            comment.VisitorName.Should().Be("Admin Test visitor");
            comment.CommentText.Should().Be("Admin Test comment");
        }

        [Fact]
        public async Task Delete_ShouldSucceed()
        {
            var commentId = await _http
                .AdminClient
                .CreateAdminCommentAsync(SeedConstants.PublishedBlogPost,
                    new CreateCommentVm()
                    {
                        VisitorName = "Admin Test visitor",
                        CommentText = "Admin Test comment"
                    });

            commentId.Should().BeGreaterThan(0);
            
            var comment = await _http
                .AdminClient
                .GetAdminCommentAsync(SeedConstants.PublishedBlogPost, commentId);

            comment.Should().NotBeNull();
            comment.Id.Should().Be(commentId);
            comment.VisitorName.Should().Be("Admin Test visitor");
            comment.CommentText.Should().Be("Admin Test comment");
            
            await _http
                .AdminClient
                .DeleteAdminCommentAsync(comment.BlogPostId, comment.Id);
            
            await _http
                .AdminClient
                .DeleteAdminCommentAsync(comment.BlogPostId, comment.Id, HttpStatusCode.NotFound);

        }

    }
}