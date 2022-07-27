using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnboardingInsuranceAPI.Areas.Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace OnboardingInsuranceAPI.Areas.User;
public class RegisterUserController
{
    private readonly RegisterUserHandler _handler;
    private readonly ILogger<RegisterUserController> _log;

    public RegisterUserController(RegisterUserHandler handler, ILogger<RegisterUserController> log)
    {
        _handler = handler;
        _log = log;
    }

    [Function("RegisterUser")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        _log.LogInformation($"C# HTTP trigger function processed a request RegisterUserController with content: {content}");
        RequestedData requestedData = JsonConvert.DeserializeObject<RequestedData>(content);
        await _handler.CreateUser(requestedData.Sub, requestedData.Email);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
