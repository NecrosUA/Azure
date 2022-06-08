using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WatchPortalFunction
{
    public class AdminArea
    {
        [FunctionName("Webadmin")]

        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the model id from the query string
            string admin = req.Query["id"];

            // If the user specified a model id, find the details of the model of watch
            if (admin == "Rost" )
            {
                return new OkObjectResult($"Welcome: {admin} to admin area!");
            }
            return new BadRequestObjectResult("Please provide correct admin id query");
        }
    }
}
