using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UploadUserImageController
{
    private readonly UploadUserImageHandler _handler;
    public UploadUserImageController(UploadUserImageHandler handler)
    {
        _handler = handler;
    }
    [Function("UploadUserImage")]
    public async Task<IActionResult> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images")] HttpRequest req,
        ILogger log)
    {
        var filename = await _handler.SaveImageToBlobContainer(req.Form.Files["File"]);
        log.LogInformation($"C# HTTP trigger function processed a request UploadUserImage with image name: {filename}");

        return new OkObjectResult(filename);
    }
}
