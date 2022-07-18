using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnboardingInsuranceAPI.Areas.Shared;

namespace OnboardingInsuranceAPI.Areas.User;
public static class RegisterUserController
{
    private static readonly Repository _repo = new Repository();
    [FunctionName("RegisterUser")]
    //public static async Task<IActionResult> Run(
    //    [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/{pid}")] HttpRequest req,string pid,
    //    ILogger log)
    //{
    //    log.LogInformation("C# HTTP trigger function processed a request RegisterUserController.");
    //    _repo.CreateUser(pid);
    //    return new OkResult();
    //}
    public static async Task<IActionResult> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request RegisterUserController.");
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        //log.LogInformation($"{content}");
        RequestedData requestedData = JsonConvert.DeserializeObject<RequestedData>(content);
        _repo.CreateUser(requestedData.Sub);
        return new OkResult();
    }
}
