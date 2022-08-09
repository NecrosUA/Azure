using HttpMultipartParser;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class ImageController
{
    private readonly UploadImage _handler;
    private readonly ILogger<ImageController> _log;

    public ImageController(UploadImage handler, ILogger<ImageController> log)
    {
        _handler = handler;
        _log = log;
    }
    [Function("UploadImage")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images")] HttpRequestData req)
    {
        var parsedFromBody = MultipartFormDataParser.ParseAsync(req.Body);
        var file = parsedFromBody.Result.Files[0];
        var filename = await _handler.SaveImageToBlobContainer(file);
        //_log.LogInformation($"C# HTTP trigger function processed a request UploadUserImage with image name: {filename}");
        return await req.ReturnJson(filename);
    }
}
