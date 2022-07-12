using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Services;

public class DataContext : DbContext
{
    public DbSet<UserInfo> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
            });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>()
            .HasPartitionKey(o => o.Pid)
            .HasData(new UserInfo
            {
                Pid = "773ba94b-e9a8-4a1c-9d7e-655eb2f426d9",
                Name = "Rostyslav",
                Surname = "Kokhanchuk",
                Birthdate = "1983-03-20",
                BirthNumber = "8303201234",
                MobileNumber = "77422914",
                Email = "Adam.Jensen@dex.cz",
                Address1 = "Zeleň 43/1",
                Address2 = "Prague - Překážka",
                ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
            },
            new UserInfo
            {
                Pid = "PID1234567890",
                Name = "Adam",
                Surname = "Jensen",
                Birthdate = "1993-03-09",
                BirthNumber = "9303091324",
                MobileNumber = "77422914",
                Email = "Adam.Jensen@dex.cz",
                Address1 = "Zeleň 43/1",
                Address2 = "Prague - Překážka",
                ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
            });

        base.OnModelCreating(modelBuilder);
    }
}

