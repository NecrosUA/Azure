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
                    .AddDbContext<DataContext>(optionsBuilder => optionsBuilder.UseCosmos(
                        Environment.GetEnvironmentVariable("AccountEndpoint"),
                        Environment.GetEnvironmentVariable("AccountKey"),
                        databaseName: "Data",
                        options =>
                        {
                            options.ConnectionMode(ConnectionMode.Gateway);
                            options.WebProxy(new WebProxy());
                            options.Region(Regions.WestEurope);
                            options.GatewayModeMaxConnectionLimit(32);
                        })); //AddDbContextFactory try booth
            })
            .ConfigureOpenApi()
            .Build();
        await host.RunAsync();
    }
}
