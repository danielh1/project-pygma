using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Pygma.Blog.ViewModels.Requests.BlogPostComments;
using Pygma.Blog.ViewModels.Responses.Comments;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpBlogPostCommentsExtensions
    {
        private const string url = "/api/blog-posts";
        
        public static async Task<CommentVm> GetCommentAsync(this HttpClient client, int blogPostId, int commentId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoGetAsync<CommentVm>($"{url}/{blogPostId}/comments/{commentId}", expectedStatusCode);
        }
        
        public static async Task<int> CreateCommentAsync(this HttpClient client, int blogPostId, CreateCommentVm createCommentVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoPostAsync<CreateCommentVm, int>($"{url}/{blogPostId}/comments", createCommentVm, expectedStatusCode);
        }
    }
}