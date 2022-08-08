using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class InsuranceController
{
    private readonly GetInsurance _handlerGet;
    private readonly PutInsurance _handlerPut;
    private readonly ILogger<InsuranceController> _logger;
    

    public InsuranceController(GetInsurance handlerGetInsurance, PutInsurance handlerPutInsurance, ILogger<InsuranceController> log)
    {
        _logger = log;
        _handlerGet = handlerGetInsurance;
        _handlerPut = handlerPutInsurance;
    }

    [Function("GetInsurance")]
    public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "insurance/{pid}")] HttpRequestData req, string pid)
    {
        _logger.LogInformation($"C# HTTP trigger function processed a request ReadUserInsurance with pid: {pid}");
        var insuranceData = await _handlerGet.GetInsuranceByPid(pid);
        return await req.ReturnJson(insuranceData);
    }

    [Function("PutInsurance")]
    public async Task<HttpResponseData> Put([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "insurance")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request WriteUserInsurance.");
        var requestedData = await req.ReadBodyAs<InsuranceData>();
        await _handlerPut.UpdateInsurance(requestedData);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
