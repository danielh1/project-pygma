using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pygma.UatTests.Extensions;
using Pygma.Users.Models;
using Pygma.Users.ViewModels.Requests;
using Pygma.Users.ViewModels.Requests.Account;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpAccountExtensions
    {
        private const string url = "/api/account";

        public static async Task RegisterAsync(this HttpClient client,
            RegisterAccountVm registerAccountVm,
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            await client.DoPostAsync<RegisterAccountVm, ActionResult>($"{url}/registration", registerAccountVm,
                expectedStatusCode);
        }

        public static async Task<Jwt> LoginAsync(this HttpClient client, LoginVm loginVm,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            return await client.DoPostAsync<LoginVm, Jwt>($"{url}/login", loginVm, expectedStatusCode);
        }
    }
}