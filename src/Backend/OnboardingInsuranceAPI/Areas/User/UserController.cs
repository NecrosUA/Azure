using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace OnboardingInsuranceAPI.Areas.User;

public class UserController
{
    private readonly GetUser _handlerGet;
    private readonly PostUser _handlerPost;
    private readonly PutUser _handlerPut;
    private readonly ILogger<UserController> _log;

    //private static readonly Idb<ClientInfo> db; 

    public UserController(GetUser handlerGet, PostUser handlerPost , PutUser handlerPut, ILogger<UserController> log)
    {
        _handlerGet = handlerGet;
        _handlerPut = handlerPut;
        _handlerPost = handlerPost;
        _log = log;
    }

    [Function("GetUser")] //read user profile by PID number
    public  async Task<HttpResponseData> Get(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{pid}")] HttpRequestData req, string pid)
    {
        _log.LogInformation("C# HTTP trigger function processed a request ReadUserSettings.");

        var userData = await _handlerGet.GetUserBy(pid);

        return await req.ReturnJson(userData);
    }

    [Function("PutUser")] //write into user profile 
    public  async Task<HttpResponseData> Put(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequestData req)
    {
        _log.LogInformation("C# HTTP trigger function processed a request WriteUserSettings.");
        var requestedData = await req.ReadBodyAs<UserData>();

        await _handlerPut.UpdateItem(requestedData); 

        return req.CreateResponse(HttpStatusCode.Accepted);
    }

    [Function("PostUser")] //Register new user
    public async Task<HttpResponseData> Post(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequestData req)
    {

        _log.LogInformation($"C# HTTP trigger function processed a request RegisterUser");
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        UserData requestedData = JsonConvert.DeserializeObject<UserData>(content);
        //var requestedData = await req.ReadBodyAs<RequestedData>();
        //_log.LogInformation($"Requested data: {requestedData}");
        await _handlerPost.CreateUser(requestedData.Sub, requestedData.Email);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
