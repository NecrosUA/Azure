using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using OnboardingInsuranceAPI.Extensions;
using System.IO;
using Newtonsoft.Json;

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
        
        _log.LogInformation($"C# HTTP trigger function processed a request RegisterUser");
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        UserData requestedData = JsonConvert.DeserializeObject<UserData>(content);
        //var requestedData = await req.ReadBodyAs<RequestedData>();
        //_log.LogInformation($"Requested data: {requestedData}");
        await _handler.CreateUser(requestedData.Sub, requestedData.Email);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
