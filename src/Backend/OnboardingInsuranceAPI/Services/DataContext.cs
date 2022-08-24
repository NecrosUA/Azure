using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Converters;

namespace OnboardingInsuranceAPI.Services;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<CarInsuranceInfo> Insurances { get; set; }
    public DbSet<UserInfo> Users { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>()
            .ToContainer("Users")
            .HasPartitionKey(o => o.Pid)
            .HasData(
                new UserInfo
                {
                    Pid = "773ba94b-e9a8-4a1c-9d7e-655eb2f426d9",
                    Name = "Adam",
                    Surname = "Jensen",
                    Birthdate = new DateOnly(1993, 03, 09),
                    BirthNumber = "9303091234",
                    MobileNumber = "77422914",
                    Email = "Adam.Jensen@dex.cz",
                    Address1 = "Zeleň 43/1",
                    Address2 = "Praha - Překážka",
                    ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
                },
                new UserInfo
                {
                    Pid = "0fbc4301-e6f4-47ad-a115-36fb6c914b99",
                    Name = "Rostislav",
                    Surname = "Kochančhuk",
                    Birthdate = new DateOnly(1983, 03, 20),
                    BirthNumber = "8303091324",
                    MobileNumber = "77422915",
                    Email = "Rost.Koch@dex.cz",
                    Address1 = "Krakovská 775/31",
                    Address2 = "Praha - Sparta",
                    ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg"
                });

            modelBuilder.Entity<CarInsuranceInfo>()
                .ToContainer("Insurances")
                .HasPartitionKey(o => o.Pid)
                .HasData(
                new List<CarInsuranceInfo>()
                {
                    new CarInsuranceInfo
                    {
                        Pid = "773ba94b-e9a8-4a1c-9d7e-655eb2f426d9",
                        InsuranceId = "ca4f0600-9493-4d9a-8580-ee5a16119520",
                        CarBarnd = "Škoda",
                        CarType = "SportCar",
                        Crashed = true,
                        FirstOwner = true,
                        InformationNote = "Confident driver",
                        ExpirationDate = new DateOnly(2023, 03, 20),
                        LastService = new DateOnly(2022, 01, 01),
                        YearOfProduction = 2020,
                        YearlyContribution = 4760
                    },
                    new CarInsuranceInfo
                    {
                        Pid = "773ba94b-e9a8-4a1c-9d7e-655eb2f426d9",
                        InsuranceId = "97e1576b-5384-40ae-ae10-c1d0935e8133",
                        CarBarnd = "BMW",
                        CarType = "SuperCar",
                        Crashed = false,
                        FirstOwner = false,
                        InformationNote = "Confident driver",
                        ExpirationDate = new DateOnly(2023, 03, 20),
                        LastService = new DateOnly(2022, 01, 01),
                        YearOfProduction = 2022,
                        YearlyContribution = 4760
                    }
                });

        base.OnModelCreating(modelBuilder);
    }
}

