using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Extensions;

internal static class StartupExtensions
{
    internal static IServiceCollection AddCosmosDb(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(optionsBuilder
            => optionsBuilder.UseCosmos(
            Environment.GetEnvironmentVariable("AccountEndpoint"),
            Environment.GetEnvironmentVariable("AccountKey"),
            databaseName: "Data",
            options =>
            {
                options.ConnectionMode(ConnectionMode.Gateway);
                options.WebProxy(new WebProxy());
                options.Region(Regions.WestEurope);
                options.GatewayModeMaxConnectionLimit(32);
            }));
        return services;
    }
    internal static IServiceCollection AddInMemoryDb(this IServiceCollection services) //TODO implement mock data
    {
        services.AddDbContext<DataContext>(optionsBuilder
            => optionsBuilder.UseInMemoryDatabase(
                databaseName: "Data"
            ));

        return services;
    }
}
