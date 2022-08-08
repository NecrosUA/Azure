using HttpMultipartParser;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Extensions;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class ImageController
{
    private readonly PostImage _handlerPost;
    private readonly ILogger<ImageController> _log;

    public ImageController(PostImage handlerPost, ILogger<ImageController> log)
    {
        _handlerPost = handlerPost;
        _log = log;
    }
    [Function("PostImage")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images")] HttpRequestData req)
    {
        var parsedFromBody = MultipartFormDataParser.ParseAsync(req.Body);
        var file = parsedFromBody.Result.Files[0];
        var filename = await _handlerPost.SaveImageToBlobContainer(file);
        _log.LogInformation($"C# HTTP trigger function processed a request UploadUserImage with image name: {filename}");
        return await req.ReturnJson(filename);
    }
}
