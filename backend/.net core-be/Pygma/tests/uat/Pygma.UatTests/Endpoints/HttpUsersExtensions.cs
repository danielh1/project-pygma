using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpUsersExtensions
    {
        // private const string url = "/api/users";
        //
        // public static async Task<UserListItemVm[]> SearchUsersAsync(this HttpClient client, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        // {
        //     return await client.DoGetAsync<UserListItemVm[]>($"{url}", expectedStatusCode);
        // }
        //
        // public static async Task<UserVm> GetUserAsync(this HttpClient client, int userId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        // {
        //     return await client.DoGetAsync<UserVm>($"{url}/{userId}", expectedStatusCode);
        // }
        //
        // public static async Task<ActionResult> UpdateUserAsync(this HttpClient client, int userId, UpdateUserVm updateUserVm, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        // {
        //     return await client.DoPutAsync<UpdateUserVm, ActionResult>($"{url}/{userId}", updateUserVm, expectedStatusCode);
        // }
    }
}