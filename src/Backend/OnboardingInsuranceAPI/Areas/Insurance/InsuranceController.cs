using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class InsuranceController
{
    private readonly GetInsurance _handlerGet;
    private readonly AddInsurance _handlerPut;
    
    public InsuranceController(GetInsurance handlerGet, AddInsurance handlerPut)
    {
        _handlerGet = handlerGet;
        _handlerPut = handlerPut;
    }

    [Function("GetInsurance")]
    public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "insurance")] HttpRequestData req)
    {
        var pid = req.ReadPidFromJwt();
        var insuranceData = await _handlerGet.Handle(pid);
        return await req.ReturnJson(insuranceData);
    }

    [Function("PutInsurance")]
    public async Task<HttpResponseData> Put([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "insurance")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<InsuranceData>();
        var pid = req.ReadPidFromJwt();
        await _handlerPut.Handle(requestedData, pid);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }
}
