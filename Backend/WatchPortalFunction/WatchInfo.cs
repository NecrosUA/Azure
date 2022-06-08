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
using WatchPortalFunction.Entities;
//using WatchPortalFunction.Auth;

namespace WatchPortalFunction
{
    public class WatchInfo //: AuthorizedServiceBase
    {

        [FunctionName("Watchinfo")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "watchinfo")] HttpRequest req, ILogger log )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // Retrieve the model id from the query string
            string id = req.Query["id"];

            // Use dummy data for this example
            var watchinfo = new List<WatchInfoEntity>
                {
                    new WatchInfoEntity { Manufacturer = "abc", CaseType = "Solid", Bezel = "Titanium", Dial = "Roman", CaseFinish = "Gold", Jewels = 40 },
                    new WatchInfoEntity { Manufacturer = "acb", CaseType = "Medium", Bezel = "Iron", Dial = "Co-Signed", CaseFinish = "Silver", Jewels = 15 },
                    new WatchInfoEntity { Manufacturer = "cba", CaseType = "Bold", Bezel = "Aluminium", Dial = "Skeleton", CaseFinish = "Platinum", Jewels = 35 },
                    new WatchInfoEntity { Manufacturer = "bac", CaseType = "Plain", Bezel = "Titanium", Dial = "Enamel", CaseFinish = "Rose-gold", Jewels = 5 }
                };

            var i = watchinfo.FindIndex(x => x.Manufacturer == id);

            ////Local authentication part need to change to Azure... ??? 
            //AuthenticationInfo auth = new AuthenticationInfo(req); 

            //if (!auth.IsValid)
            //{
            //    return new UnauthorizedResult(); // No authentication info.
            //}

            //var t = auth.GetType();
            //var pi = t.GetProperties();
            //foreach (var prop in pi)
            //{
            //    Console.WriteLine($"property:  {prop.Name}");
            //}

            // If the user specified a model id, find the details of the model of watch
            if (id != null && i != -1)
            {
                return new OkObjectResult($"Watch Details: {watchinfo[i].Manufacturer}, {watchinfo[i].CaseType}, {watchinfo[i].Bezel}, {watchinfo[i].Dial}, {watchinfo[i].CaseFinish}, {watchinfo[i].Jewels}");
            }
            return new BadRequestObjectResult("Please provide a watch model in the query string");
        }
    }
}