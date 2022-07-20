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

namespace OnboardingInsuranceAPI.Areas.User;
public class RegisterUserController
{
    private readonly RegisterUserHandler _handler;
    public RegisterUserController(RegisterUserHandler handler)
    {
        _handler = handler;
    }

    [Function("RegisterUser")]

    public async Task<IActionResult> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequest req,
        ILogger log)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        log.LogInformation($"C# HTTP trigger function processed a request RegisterUserController with content: {content}");
        RequestedData requestedData = JsonConvert.DeserializeObject<RequestedData>(content);
        await _handler.CreateUser(requestedData.Sub);
        return new OkResult();
    }
}
