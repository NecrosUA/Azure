using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using OnboardingInsuranceAPI.Services;
using OnboardingInsuranceAPI.ErrorHandling;

namespace OnboardingInsuranceAPI;

public class Program
{
    static async Task Main()
    {
        var host = new HostBuilder()
            .ConfigureFunctionsWorkerDefaults(w =>
            {
                w.UseMiddleware<ExceptionHandlerMiddleware>();
            })
            .ConfigureServices(s =>
            {
                s.AddSingleton<DataContext>();
            })
            .ConfigureOpenApi()
            .Build();
        await host.RunAsync();
    }
}
