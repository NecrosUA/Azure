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
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;

namespace OnboardingInsuranceAPI.Areas.User;

public static class ReadWriteUserController
{
    //private static readonly Idb<ClientInfo> db; 
    private static readonly Repository _repo = new Repository(); 

    [FunctionName("ReadUserSettings")] //read user profile by PID number
    public static async Task<IActionResult> Read(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{pid}")] HttpRequest req, string pid,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request ReadUserSettings.");

        var userData = await _repo.GetUserBy(pid); //TODO finish this

        //string responseMessage = string.IsNullOrEmpty(pid)
        //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response." : "";
        return new OkObjectResult(userData);
    }

    [FunctionName("WriteUserSettings")] //write into user profile 
    public static async Task<IActionResult> Update(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request WriteUserSettings.");
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting request info about user from frontend 
        RequestedData requestedData = JsonConvert.DeserializeObject<RequestedData>(content);

        _repo.UpdateItem(requestedData); //test

        return new OkResult();
    }
}
