using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.User;

public class ReadWriteUserController
{
    private readonly ReadWriteUserHandler _handler;

    //private static readonly Idb<ClientInfo> db; 

    public ReadWriteUserController(ReadWriteUserHandler handler)
    {
        _handler = handler;
    }

    [Function("ReadUserSettings")] //read user profile by PID number
    public  async Task<HttpResponseData> Read(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{pid}")] HttpRequestData req, string pid)
    {
        //log.LogInformation("C# HTTP trigger function processed a request ReadUserSettings."); //Got exception here why why?

        var userData = await _handler.GetUserBy(pid);

        //string responseMessage = string.IsNullOrEmpty(pid)
        //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response." : "";

        //return new OkObjectResult(userData);
        return await req.ReturnJson(userData);
    }

    [Function("WriteUserSettings")] //write into user profile 
    public  async Task<IActionResult> Update(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequest req,
    ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request WriteUserSettings.");
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting request info about user from frontend 
        RequestedData requestedData = JsonConvert.DeserializeObject<RequestedData>(content);

        await _handler.UpdateItem(requestedData); //test

        return new OkResult();
    }
}
