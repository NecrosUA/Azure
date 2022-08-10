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
    private readonly GetUser _getUser;
    private readonly RegisterUser _registerUser;
    private readonly UpdateUser _updateUser;

    public UserController(GetUser getUser, RegisterUser registerUser , UpdateUser updateUser)
    {
        _getUser = getUser;
        _updateUser = updateUser;
        _registerUser = registerUser;
    }

    [Function("GetUser")] //read user profile by PID number
    public  async Task<HttpResponseData> Get(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{pid}")] HttpRequestData req, string pid)
    {
        var userData = await _getUser.Handle(pid);
        return await req.ReturnJson(userData);
    }

    [Function("PutUser")] //write into user profile 
    public  async Task<HttpResponseData> Put(
    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<UserData>();
        await _updateUser.Handle(requestedData); 
        return req.CreateResponse(HttpStatusCode.Accepted);
    }

    [Function("PostUser")] //Checking registartion user in cosmos db, if not - register new user
    public async Task<HttpResponseData> Post(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequestData req)
    {
        var content = await new StreamReader(req.Body).ReadToEndAsync();//Getting pid and other info inside body
        UserData requestedData = JsonConvert.DeserializeObject<UserData>(content);
        await _registerUser.Handle(requestedData.Sub, requestedData.Email);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
