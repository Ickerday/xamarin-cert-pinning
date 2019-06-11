using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CertPin.API
{
    public static class Function1
    {
        [FunctionName(nameof(Function1))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "GET", Route = null)] HttpRequest req,
            ILogger log)
        {
            foreach (var header in req.Headers)
                log.LogDebug(header.ToString());

            return await Task.FromResult(new OkResult());
        }
    }
}
