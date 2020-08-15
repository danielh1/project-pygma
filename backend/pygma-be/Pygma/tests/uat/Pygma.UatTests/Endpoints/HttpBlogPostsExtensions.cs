using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Pygma.Blog.ViewModels.Requests.BlogPosts;
using Pygma.Blog.ViewModels.Responses.BlogPosts;
using Pygma.Common.Models.Search;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpBlogPostsExtensions
    {
        private const string url = "/api/blog-posts";
        
        public static async Task<SearchResultsVm<BlogPostSrVm[]>> SearchBlogPostsAsync(this HttpClient client, SearchBlogPostVm searchBlogPostVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            
            if(searchBlogPostVm.Title != null)
                query[nameof(searchBlogPostVm.Title)] = searchBlogPostVm.Title;
            if(searchBlogPostVm.Status != null)
                query[nameof(searchBlogPostVm.Status)] = searchBlogPostVm.Status.ToString();
            if(searchBlogPostVm.PublishedAtFrom != null)
                query[nameof(searchBlogPostVm.PublishedAtFrom)] = searchBlogPostVm.PublishedAtFrom.ToString();
            if(searchBlogPostVm.PublishedAtTo != null)
                query[nameof(searchBlogPostVm.PublishedAtTo)] = searchBlogPostVm.PublishedAtTo.ToString();
            
            query[nameof(searchBlogPostVm.CurrentPage)] = searchBlogPostVm.CurrentPage.ToString();
            query[nameof(searchBlogPostVm.PageSize)] = searchBlogPostVm.PageSize.ToString();
            
            return await client.DoGetAsync<SearchResultsVm<BlogPostSrVm[]>>($"{url + "?" + query}", expectedStatusCode);
        }
        
        public static async Task<BlogPostVm> GetBlogPostAsync(this HttpClient client, int blogPostId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoGetAsync<BlogPostVm>($"{url}/{blogPostId}", expectedStatusCode);
        }
        
        public static async Task<ActionResult> UpdateBlogPostAsync(this HttpClient client, int blogPostId, UpdateBlogPostVm updateBlogPostVm, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            return await client.DoPutAsync<UpdateBlogPostVm, ActionResult>($"{url}/{blogPostId}", updateBlogPostVm, expectedStatusCode);
        }
        
        public static async Task<int> CreateBlogPostAsync(this HttpClient client, CreateBlogPostVm createBlogPostVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoPostAsync<CreateBlogPostVm, int>($"{url}/", createBlogPostVm, expectedStatusCode);
        }
        
        public static async Task<ActionResult> DeleteBlogPostAsync(this HttpClient client, int blogPostId, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            return await client.DoDeleteAsync<ActionResult>($"{url}/{blogPostId}", expectedStatusCode);
        }
    }
}