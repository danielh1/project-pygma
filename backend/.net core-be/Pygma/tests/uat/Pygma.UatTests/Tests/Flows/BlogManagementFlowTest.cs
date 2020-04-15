using System.Net;
using System.Threading.Tasks;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Data.Domain.Enums;
using Pygma.UatTests.Base;
using Pygma.UatTests.Endpoints;
using Pygma.UatTests.Infrastructure;
using Pygma.UatTests.TestDb.Seed;
using Pygma.Users.ViewModels.Requests;
using Xunit;

namespace Pygma.UatTests.Tests.Flows
{
    public class BlogManagementFlowTest : UatBase
    {
        private readonly HttpTestClient _http;

        public BlogManagementFlowTest(HttpTestClient http)
        {
            _http = http;
        }

        [Fact]
        public async Task ManageBlogPost()
        {
            // 1. Create Blog Post InEdit
            var blogPostId = await _http
                .AuthorClient
                .CreateBlogPostAsync(new CreateBlogPostVm()
                {
                    Title = "Test Blog Post management",
                    Post = "A nice flow to test our application",
                    Status = EnBlogPostStatus.InEdit
                });
            // 2. Try to access the Blog Post from default client, should fail (Not Found)
            await _http
                .DefaultClient
                .GetBlogPostAsync(blogPostId, HttpStatusCode.Forbidden);
            // 3. Publish Blog
            await _http
                .AuthorClient
                .UpdateBlogPostAsync(blogPostId, new UpdateBlogPostVm()
                {
                    Title = "Test Blog Post management Updated",
                    Post = "A nice flow to test our application Updated",
                    Status = EnBlogPostStatus.Published,
                    AuthorId = SeedConstants.AuthorUser
                });
            // 4. Try to access the Blog Post from default client, should succeed
            await _http
                .DefaultClient
                .GetBlogPostAsync(blogPostId);
            // 5. Delete Blog Post
            await _http
                .AdminClient
                .DeleteBlogPostAsync(blogPostId);
            // 6. Try to access the Blog Post from default client, not found
            await _http
                .DefaultClient
                .GetBlogPostAsync(blogPostId, HttpStatusCode.NotFound);
        }
    }
}