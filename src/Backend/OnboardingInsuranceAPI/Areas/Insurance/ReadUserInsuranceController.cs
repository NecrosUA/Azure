using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class ReadUserInsuranceController
{
    private readonly ReadUserInsuranceHandler _handler;
    private readonly ILogger<ReadUserInsuranceController> _logger;
    

    public ReadUserInsuranceController(ReadUserInsuranceHandler handler, ILogger<ReadUserInsuranceController> log)
    {
        _logger = log;
        _handler = handler;
    }

    [Function("ReadUserInsurance")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "insurance/{pid}")] HttpRequestData req, string pid)
    {
        _logger.LogInformation($"C# HTTP trigger function processed a request ReadUserInsurance with pid: {pid}");

        var insuranceData = await _handler.GetInsuranceByPid(pid);
        return await req.ReturnJson(insuranceData);
    }
}
