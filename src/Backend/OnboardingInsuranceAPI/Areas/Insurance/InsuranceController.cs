using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class InsuranceController
{
    private readonly GetInsurance _getInsurance;
    private readonly AddInsurance _addInsurance;
    private readonly GetContribution _getContribution;
    
    public InsuranceController(GetInsurance getInsurance, AddInsurance addInsurance, GetContribution getContribution)
    {
        _getInsurance = getInsurance;
        _addInsurance = addInsurance;
        _getContribution = getContribution;
    }

    [Function("GetInsurance")]
    public async Task<HttpResponseData> ReadInsurance([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "insurances")] HttpRequestData req)
    {
        var pid = req.ReadPidFromJwt();
        var insuranceData = await _getInsurance.Handle(pid);
        return await req.ReturnJson(insuranceData);
    }

    [Function("PutInsurance")]
    public async Task<HttpResponseData> UpdateInsurance([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "insurances")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<InsuranceDataRequest>();
        var pid = req.ReadPidFromJwt();
        await _addInsurance.Handle(requestedData, pid);
        return req.CreateResponse(HttpStatusCode.Accepted);
    }

    [Function("GetContribution")]
    public async Task<HttpResponseData> ReadContribution([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "contribution")] HttpRequestData req)
    {
        var requestedData = await req.ReadBodyAs<ContributionDataRequest>();
        var pid = req.ReadPidFromJwt();
        var contributionData = _getContribution.Handle(requestedData, pid);
        return await req.ReturnJson(contributionData);
    }
}
