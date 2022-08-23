using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;
using System.Net;

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
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req) //TODO rewrite frontend remove pid and fix reference 
    {
        var pid = req.ReadPidFromJwt(); //Return pid from header jwt
        var userData = await _getUser.Handle(pid);
        return await req.ReturnJson(userData);
    }

    [Function("PutUser")] //write into user profile  
    public  async Task<HttpResponseData> Put(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<UserData>();
        var pid = req.ReadPidFromJwt();
        await _updateUser.Handle(requestedData, pid); 
        return req.CreateResponse(HttpStatusCode.Accepted);
    }

    [Function("PostUser")] //Checking registartion user in cosmos db, if not - register new user
    public async Task<HttpResponseData> Post(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<RegisterUserData>();
        var pid = req.ReadPidFromJwt();
        await _registerUser.Handle(pid, requestedData.Email);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
