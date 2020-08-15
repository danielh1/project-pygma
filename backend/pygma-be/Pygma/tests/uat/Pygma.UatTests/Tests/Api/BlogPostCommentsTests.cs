using System.Threading.Tasks;
using FluentAssertions;
using Pygma.Blog.ViewModels.Requests.Comments;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Xunit;

namespace Pygma.UatTests.Tests.Api
{
    public class BlogPostCommentsTests : UatBase
    {
        private readonly HttpTestClient _http;

        public BlogPostCommentsTests(HttpTestClient http)
        {
            _http = http;
        }

        [Fact]
        public async Task Get_ShouldSucceed()
        {
            var comment = await _http
                .DefaultClient
                .GetCommentAsync(SeedConstants.PublishedBlogPost, SeedConstants.PublishedComment1);

            comment.Should().NotBeNull();
            comment.Id.Should().BeGreaterThan(0);
        }
        
        [Fact]
        public async Task Create_ShouldSucceed()
        {
            var commentId = await _http
                .DefaultClient
                .CreateCommentAsync(SeedConstants.PublishedBlogPost,
                    new CreateCommentVm()
                    {
                        VisitorName = "Test visitor",
                        CommentText = "Test comment"
                    });

            commentId.Should().BeGreaterThan(0);
            
            var comment = await _http
                .DefaultClient
                .GetCommentAsync(SeedConstants.PublishedBlogPost, commentId);

            comment.Should().NotBeNull();
            comment.Id.Should().Be(commentId);
            comment.VisitorName.Should().Be("Test visitor");
            comment.CommentText.Should().Be("Test comment");
        }
    }
}