using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.ViewModels.Requests.Comments;
using Pygma.Blog.ViewModels.Responses.Comments;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpAdminBlogPostCommentsExtensions
    {
        private const string url = "/api/admin/blog-posts";
        
        public static async Task<CommentVm> GetAdminCommentAsync(this HttpClient client, int blogPostId, int commentId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoGetAsync<CommentVm>($"{url}/{blogPostId}/comments/{commentId}", expectedStatusCode);
        }
        
        public static async Task<int> CreateAdminCommentAsync(this HttpClient client, int blogPostId, CreateCommentVm createCommentVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoPostAsync<CreateCommentVm, int>($"{url}/{blogPostId}/comments", createCommentVm, expectedStatusCode);
        }
        
        public static async Task<ActionResult> DeleteAdminCommentAsync(this HttpClient client, int blogPostId, int commentId, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            return await client.DoDeleteAsync<ActionResult>($"{url}/{blogPostId}/comments/{commentId}", expectedStatusCode);
        }
    }
}