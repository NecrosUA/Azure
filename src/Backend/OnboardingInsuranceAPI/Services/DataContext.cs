using Microsoft.EntityFrameworkCore;

namespace OnboardingInsuranceAPI.Services;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserInfo> Users { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseCosmos(
    //    Environment.GetEnvironmentVariable("AccountEndpoint"),
    //    Environment.GetEnvironmentVariable("AccountKey"),
    //    databaseName: "Data",
    //    options =>
    //    {
    //        options.ConnectionMode(ConnectionMode.Gateway);
    //        options.WebProxy(new WebProxy());
    //        options.Region(Regions.WestEurope);
    //        options.GatewayModeMaxConnectionLimit(32);
    //    });
    //}

    //protected override void OnModelCreating(ModelBuilder modelBuilder) //Uncomment it in case DB is empty
    //{
    //    modelBuilder.Entity<UserInfo>()
    //        .HasPartitionKey(o => o.Pid)
    //        .HasData(new UserInfo
    //        {
    //            Pid = "773ba94b-e9a8-4a1c-9d7e-655eb2f426d9",
    //            Name = "Adam",
    //            Surname = "Jensen",
    //            Birthdate = "1993-03-09",
    //            BirthNumber = "9303091234",
    //            MobileNumber = "77422914",
    //            Email = "Adam.Jensen@dex.cz",
    //            Address1 = "Zeleň 43/1",
    //            Address2 = "Praha - Překážka",
    //            ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
    //        },
    //        new UserInfo
    //        {
    //            Pid = "0fbc4301-e6f4-47ad-a115-36fb6c914b99",
    //            Name = "Rostislav",
    //            Surname = "Kochančhuk",
    //            Birthdate = "1983-03-09",
    //            BirthNumber = "8303091324",
    //            MobileNumber = "77422915",
    //            Email = "Rost.Koch@dex.cz",
    //            Address1 = "Krakovská 775/31",
    //            Address2 = "Praha - Sparta",
    //            ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg"
    //        });

    //    base.OnModelCreating(modelBuilder);
    //}
}

