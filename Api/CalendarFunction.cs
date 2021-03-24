using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ical.Net;

using BlazorApp.Shared;

namespace BlazorApp.Api
{
    public static class CalendarFunction
    {
        private static HttpClient httpClient = new HttpClient();
        
        [FunctionName("Calendar")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            var response = await httpClient.GetAsync("https://p38-caldav.icloud.com/published/2/MTkzMjQ1NTg4MTE5MzI0NQjL2bmhdcNZJHVdsWd9S9l0ZL2pxqk2jwzD3AeQDdQtrvxSUvSvOtq-XfoX5CGzbd68jOAu27J5c3pkn0O8EyE");
            string resultContent = await response.Content.ReadAsStringAsync();

            var calendar = Calendar.Load(resultContent); 

            return new OkObjectResult(resultContent);
        }
    }
}
