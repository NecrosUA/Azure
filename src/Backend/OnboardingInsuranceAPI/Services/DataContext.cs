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
            .HasPartitionKey(o => o.Pid);

            modelBuilder.Entity<CarInsuranceInfo>()
                .ToContainer("Insurances")
                .HasPartitionKey(o => o.Pid);

        base.OnModelCreating(modelBuilder);
    }
}

