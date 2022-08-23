using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;
using System.Text.Json;
using OnboardingInsuranceAPI.System.Text.Json;

namespace OnboardingInsuranceAPI;

public class Program
{
    static async Task Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(worker =>
            {
                worker
                    .UseMiddleware<ExceptionHandlerMiddleware>()
                    .UseMiddleware<CustomAuthorizationMiddleware>(); ;
            })
            .ConfigureServices(services =>
            {
                services
                    .Configure<JsonSerializerOptions>(jsonOptions =>
                    {
                        jsonOptions.Converters.Add(new DateOnlyJsonConverter());
                    })
                    .AddScopedByInterface<IHandler>()
                    .AddCosmosDb();
            })
            .ConfigureOpenApi()
            .Build();
        await host.RunAsync();
    }
}
