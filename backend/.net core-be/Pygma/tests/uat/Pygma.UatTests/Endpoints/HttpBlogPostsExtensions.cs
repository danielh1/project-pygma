using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Pygma.Common.Models.Search;
using Pygma.UatTests.Extensions;

namespace Pygma.UatTests.Endpoints
{
    public static class HttpBlogPostsExtensions
    {
        // private const string url = "/api/invoices";
        //
        // public static async Task<SearchResultsVm<InvoiceSrVm[]>> SearchInvoicesAsync(this HttpClient client, SearchInvoiceVm searchInvoiceVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        // {
        //     var query = HttpUtility.ParseQueryString(string.Empty);
        //     
        //     if(searchInvoiceVm.InvoiceNumber != null)
        //         query[nameof(searchInvoiceVm.InvoiceNumber)] = searchInvoiceVm.InvoiceNumber;
        //     
        //     return await client.DoGetAsync<SearchResultsVm<InvoiceSrVm[]>>($"{url + "?" + query}", expectedStatusCode);
        // }
        //
        // public static async Task<InvoiceVm> GetInvoiceAsync(this HttpClient client, int invoiceId, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        // {
        //     return await client.DoGetAsync<InvoiceVm>($"{url}/{invoiceId}", expectedStatusCode);
        // }
        //
        // public static async Task<ActionResult> UpdateInvoiceAsync(this HttpClient client, int invoiceId, UpdateInvoiceVm updateInvoiceVm, HttpStatusCode expectedStatusCode = HttpStatusCode.NoContent)
        // {
        //     return await client.DoPutAsync<UpdateInvoiceVm, ActionResult>($"{url}/{invoiceId}", updateInvoiceVm, expectedStatusCode);
        // }
        //
        // public static async Task<int> CreateInvoiceAsync(this HttpClient client, CreateInvoiceVm createInvoiceVm, HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        // {
        //     return await client.DoPostAsync<CreateInvoiceVm, int>($"{url}/", createInvoiceVm, expectedStatusCode);
        // }
    }
}