using Azure.Storage.Blobs;
using HttpMultipartParser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
//using Microsoft.Azure.WebJobs;
//using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class UploadUserImageController
{
    private readonly UploadUserImageHandler _handler;
    private readonly ILogger<UploadUserImageController> _log;

    public UploadUserImageController(UploadUserImageHandler handler, ILogger<UploadUserImageController> log)
    {
        _handler = handler;
        _log = log;
    }
    [Function("UploadUserImage")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images")] HttpRequestData req)
    {
        var parsedFromBody = MultipartFormDataParser.ParseAsync(req.Body);
        var file = parsedFromBody.Result.Files[0];
        var filename = await _handler.SaveImageToBlobContainer(file);
        _log.LogInformation($"C# HTTP trigger function processed a request UploadUserImage with image name: {filename}");
        return await req.ReturnJson(filename);
    }
}
