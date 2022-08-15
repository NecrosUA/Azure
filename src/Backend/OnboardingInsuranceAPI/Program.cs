using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Azure.Cosmos;
using System.Net;

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
                    .AddScopedByInterface<IHandler>()
                    .AddCosmosDb(); //Dev DB
            })
            .ConfigureOpenApi()
            .Build();
        await host.RunAsync();
    }
}
