using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Extensions;

internal static class FunctionContextExtensions
{
    internal static async Task SetResponse(this FunctionContext context, HttpStatusCode statusCode, object response = null)
    {
        var req = await context.GetHttpRequestDataAsync();

        //This should never happen for HTTP request
        if (req is null)
            return;

        var res = req.CreateResponse(statusCode);

        if (response != null)
            await res.WriteAsJsonAsync(response, statusCode);

        context.GetInvocationResult().Value = res;
    }
}