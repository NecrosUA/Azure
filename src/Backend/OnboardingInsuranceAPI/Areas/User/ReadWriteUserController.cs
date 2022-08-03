using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;
using System.Net;

namespace OnboardingInsuranceAPI.Areas.User;

public class ReadWriteUserController
{
    private readonly ReadWriteUserHandler _handler;
    private readonly ILogger<ReadWriteUserController> _log;

    //private static readonly Idb<ClientInfo> db; 

    public ReadWriteUserController(ReadWriteUserHandler handler, ILogger<ReadWriteUserController> log)
    {
        _handler = handler;
        _log = log;
    }

    [Function("ReadUserSettings")] //read user profile by PID number
    public  async Task<HttpResponseData> Read(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{pid}")] HttpRequestData req, string pid)
    {
        _log.LogInformation("C# HTTP trigger function processed a request ReadUserSettings.");

        var userData = await _handler.GetUserBy(pid);

        return await req.ReturnJson(userData);
    }

    [Function("WriteUserSettings")] //write into user profile 
    public  async Task<HttpResponseData> Update(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequestData req)
    {
        _log.LogInformation("C# HTTP trigger function processed a request WriteUserSettings.");
        var requestedData = await req.ReadBodyAs<RequestedData>();

        await _handler.UpdateItem(requestedData); 

        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
