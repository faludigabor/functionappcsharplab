using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BCFantastic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace LabFunctionApp
{
    public static class BFunctionApp
    {
        [FunctionName("BFunctionApp")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();
            var paramA = data?.i;
            var paramB = data?.j;

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            if ((paramA != null) && (paramB != null))
            {
                int i = int.Parse(paramA.ToString());
                int j = int.Parse(paramB.ToString());
                httpResponseMessage = req.CreateResponse(HttpStatusCode.OK, Calculator.Add(i, j));
            }
            return httpResponseMessage;
        }
    }
}
