using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;

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
                //.AddScoped<ReadWriteUserHandler>()
                //.AddScoped<RegisterUserHandler>()
                //.AddScoped<UploadUserImageHandler>()
                .AddDbContextFactory<DataContext>(); 
            })
            .ConfigureOpenApi()
            .Build();
        await host.RunAsync();
    }
}
