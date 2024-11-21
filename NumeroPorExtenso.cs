using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using System.Net;
using Caelum.Stella.CSharp.Inwords;

namespace Company.Function
{
    public static class NumeroPorExtenso
    {
        [FunctionName("NumeroPorExtenso")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Tradução" })]
        [OpenApiParameter(name: "numero", In = ParameterLocation.Query, Required = true, Type = typeof(double), Description = "**Número** para a tradução em extenso")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "Retorna o valor do número em extenso")]

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            double numero = Convert.ToInt64(req.Query["numero"]);

            string numeroPorExtenso = new Numero(numero).Extenso();

            string responseMessage = $"O número {numero} por extenso é: {numeroPorExtenso}";

            return new OkObjectResult(responseMessage);
        }
    }
}
