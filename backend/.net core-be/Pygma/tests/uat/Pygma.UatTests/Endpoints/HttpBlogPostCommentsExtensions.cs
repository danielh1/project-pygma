using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Search;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    // public static class HttpBlogPostCommentsExtensions
    // {
    //     private const string url = "/api/BlogPostComments";
    //
    //     public static async Task<SearchResultsVm<BlogPostCommentSrVm[]>> SearchBlogPostCommentsAsync(this HttpClient client, SearchBlogPostCommentVm searchBlogPostCommentVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    //     {
    //         var query = HttpUtility.ParseQueryString(string.Empty);
    //         
    //         if(searchBlogPostCommentVm.BlogPostCommentNumber != null)
    //             query[nameof(searchBlogPostCommentVm.BlogPostCommentNumber)] = searchBlogPostCommentVm.BlogPostCommentNumber;
    //         
    //         return await client.DoGetAsync<SearchResultsVm<BlogPostCommentSrVm[]>>($"{url + "?" + query}", expectedStatusCode);
    //     }
    //     
    //     public static async Task<BlogPostCommentVm> GetBlogPostCommentAsync(this HttpClient client, int BlogPostCommentId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    //     {
    //         return await client.DoGetAsync<BlogPostCommentVm>($"{url}/{BlogPostCommentId}", expectedStatusCode);
    //     }
    //     
    //     public static async Task<ActionResult> UpdateBlogPostCommentAsync(this HttpClient client, int BlogPostCommentId, UpdateBlogPostCommentVm updateBlogPostCommentVm, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
    //     {
    //         return await client.DoPutAsync<UpdateBlogPostCommentVm, ActionResult>($"{url}/{BlogPostCommentId}", updateBlogPostCommentVm, expectedStatusCode);
    //     }
    //     
    //     public static async Task<int> CreateBlogPostCommentAsync(this HttpClient client, CreateBlogPostCommentVm createBlogPostCommentVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
    //     {
    //         return await client.DoPostAsync<CreateBlogPostCommentVm, int>($"{url}/", createBlogPostCommentVm, expectedStatusCode);
    //     }
    // }
}