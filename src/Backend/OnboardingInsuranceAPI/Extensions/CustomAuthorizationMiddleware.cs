using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Functions;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Extensions;

public class CustomAuthorizationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly ILogger<CustomAuthorizationMiddleware> _logger;
    private readonly IHostEnvironment _host;

    public CustomAuthorizationMiddleware(ILogger<CustomAuthorizationMiddleware> logger, IHostEnvironment host)
    {
        _logger = logger;
        _host = host;
    }
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var fullMethodName = context.FunctionDefinition.EntryPoint;

        
        if (IsBlackListed(fullMethodName))
        {
            _logger.LogWarning("DEV {methodName} invoked on non-dev Environment", fullMethodName);

            //Return 404, so dev methods are not discoverable from the outside
            await context.SetResponse(HttpStatusCode.NotFound);
            return;
        }
        await next(context);
    }
    private static bool IsBlackListed(string methodName) //TODO not blacklisting
    {
        var blackList = new[]
        {
            nameof(OpenApiTriggerFunction.RenderOAuth2Redirect),
            nameof(OpenApiTriggerFunction.RenderOpenApiDocument),
            nameof(OpenApiTriggerFunction.RenderSwaggerDocument),
            nameof(OpenApiTriggerFunction.RenderSwaggerUI)
        };

        return blackList.Any(b => b.Equals(methodName));
    }
}
