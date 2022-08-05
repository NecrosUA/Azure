using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class WriteUserInsuranceController
{
    private readonly ILogger _logger;
    private readonly WriteUserInsuranceHandler _handler;

    public WriteUserInsuranceController(ILogger<WriteUserInsuranceController> logger, WriteUserInsuranceHandler handler)
    {
        _logger = logger;
        _handler = handler;
    }

    [Function("WriteUserInsurance")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "insurance")] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request WriteUserInsurance.");
        var requestedData = await req.ReadBodyAs<InsuranceData>();
        await _handler.UpdateInsurance(requestedData);
        return req.CreateResponse(HttpStatusCode.Accepted); 
    }
}
