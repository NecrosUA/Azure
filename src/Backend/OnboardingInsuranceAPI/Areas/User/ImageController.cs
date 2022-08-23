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

    public ImageController(UploadImage handler)
    {
        _handler = handler;
    }

    [Function("UploadImage")]
    public async Task<HttpResponseData> Create(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "images")] HttpRequestData req)
    {
        var filename = await _handler.Handle(req.Body);
        return await req.ReturnJson(filename);
    }
}
