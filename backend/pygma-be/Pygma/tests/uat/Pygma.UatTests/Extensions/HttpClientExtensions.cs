using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Pygma.UatTests.Infrastructure;

namespace Pygma.UatTests.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResponse> DoGetAsync<TResponse>(this HttpClient client, string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var response = await client.GetAsync(url);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            return expectedStatusCode == HttpStatusCode.OK 
                ? new JsonMessageParser<TResponse>().Deserialize(responseAsString) 
                : default;
        }

        public static async Task<TResponse> DoPostAsync<TPayload, TResponse>(this HttpClient client, string url,
            TPayload payload, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var httpContent = new StringContent(new JsonMessageParser<TPayload>().Serialize(payload));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(url, httpContent);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }
            
            var responseAsString = await response.Content.ReadAsStringAsync();
            
            return response.StatusCode == HttpStatusCode.OK
                ? new JsonMessageParser<TResponse>().Deserialize(responseAsString)
                : default;
        }

        public static async Task<TResponse> DoPutAsync<TPayload, TResponse>(this HttpClient client, string url,
            TPayload payload, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var httpContent = new StringContent(new JsonMessageParser<TPayload>().Serialize(payload));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(url, httpContent);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            return response.StatusCode == HttpStatusCode.OK
                ? new JsonMessageParser<TResponse>().Deserialize(responseAsString)
                : default;
        }

        public static async Task<TResponse> DoPutAsync<TResponse>(this HttpClient client, string url, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var httpContent = new StringContent(string.Empty);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(url, httpContent);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            return new JsonMessageParser<TResponse>().Deserialize(responseAsString);
        }

        public static async Task<TResponse> DoPatchAsync<TPayload, TResponse>(this HttpClient client, string url,
            TPayload payload, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var httpContent = new StringContent(new JsonMessageParser<TPayload>().Serialize(payload));
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PatchAsync(url, httpContent);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            return new JsonMessageParser<TResponse>().Deserialize(responseAsString);
        }

        public static async Task<TResponse> DoDeleteAsync<TResponse>(this HttpClient client, string url,
            HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        {
            var response = await client.DeleteAsync(url, CancellationToken.None);

            if (response.StatusCode != expectedStatusCode)
            {
                throw new Xunit.Sdk.XunitException($"Response status {response.StatusCode} url: {url}");
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            return response.StatusCode == HttpStatusCode.OK
                ? new JsonMessageParser<TResponse>().Deserialize(responseAsString)
                : default;
        }
    }
}