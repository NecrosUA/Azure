using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Extensions;

public static class HttpRequestDataExtensions
{
    internal static async Task<HttpResponseData> ReturnJson(this HttpRequestData requestData, object payload, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var response = requestData.CreateResponse();
        await response.WriteAsJsonAsync(payload, statusCode);

        //todo: move to APIM after CI/CD is setup
        response.Headers.Add(HeaderNames.CacheControl, "no-cache");

        return response;
    }

    internal static async Task<TJson> ReadBodyAs<TJson>(
        this HttpRequestData requestData,
        JsonSerializerOptions options = default)
    {
        options ??= new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };
        return (await JsonSerializer.DeserializeAsync<TJson>(requestData.Body, options))!; 
    }
}
