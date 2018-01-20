using System.Data.Services.Client;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AzureWebAppTest
{
    public static class MyTestFunction3
    {
        // http://localhost:7071/api/MyTestFunction3
        [FunctionName("MyTestFunction3")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Webhook was triggered2!");
            
            string jsonContent = await req.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonContent);

            log.Info($"Number is: {data.result.parameters.number}");
            log.Info($"Color is: {data.result.parameters.color}");

            var responseText = $"You silly name is: {data.result.parameters.color} {data.result.parameters.number}.";
            return req.CreateResponse(HttpStatusCode.OK, new
            {
                speech = responseText,
                displayText = responseText,
                data = (object)null,
                contextOut = new object[0],
                source = "thesource"
            });
        }
    }
}
