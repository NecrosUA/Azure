namespace OnboardingInsuranceAPITests.Insurance;

public class GetInsuranceTests : IDisposable
{
    private readonly DataContext _context;

    private readonly List<CarInsuranceInfo> _mockCarInsurance = new List<CarInsuranceInfo>()
    {
        new CarInsuranceInfo
        {
            Pid = "6a891aca-60c9-4c70-87c4-6bd52f9f2425",
            InsuranceId = "f4098d60-3b9b-4c99-a4d6-466deac63894",
            CarBarnd = "BMW",
            CarType = "SportCar",
            Crashed = true,
            FirstOwner = true,
            InformationNote = "Mock driver",
            ExpirationDate = new DateOnly(2023, 03, 20),
            LastService = new DateOnly(2022, 01, 01),
            YearOfProduction = 2020,
            YearlyContribution = 4760
        },
        new CarInsuranceInfo
        {
            Pid = "6a891aca-60c9-4c70-87c4-6bd52f9f2425",
            InsuranceId = "3b654eaf-e6c1-4920-a070-1c97c4f1c240",
            CarBarnd = "Mercedes",
            CarType = "SportCar",
            Crashed = false,
            FirstOwner = false,
            InformationNote = "Mock driver",
            ExpirationDate = new DateOnly(2023, 03, 20),
            LastService = new DateOnly(2022, 01, 01),
            YearOfProduction = 2020,
            YearlyContribution = 4760
        }
    };

    private readonly UserInfo _mockUserInfo = new UserInfo
    {
        Pid = "6a891aca-60c9-4c70-87c4-6bd52f9f2425",
        Name = "Mock",
        Surname = "User",
        Birthdate = new DateOnly(1983, 03, 20),
        BirthNumber = "8303091324",
        MobileNumber = "77422915",
        Email = "Mock.Usr@dex.cz",
        Address1 = "Krakovská 775/31",
        Address2 = "Praha - Sparta",
        ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg"
    };

    public GetInsuranceTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetInsuranceTestsData")
            .Options;

        _context = new DataContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Users.Add(_mockUserInfo);
        _context.Insurances.AddRange(_mockCarInsurance);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_NullPid_ExceptionReturned()
    {
        string pid = default!;
        var getInsurance = new GetInsurance(_context);

        var exception = await Record.ExceptionAsync(() => getInsurance.Handle(pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handle_WrongPid_ExceptionReturned()
    {
        string pid = "wrong pid";
        var getInsurance = new GetInsurance(_context);

        var exception = await Record.ExceptionAsync(() => getInsurance.Handle(pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("NotFound",exception.Message);
    }

    [Fact]
    public async Task Handle_CorrectPid_CorrectResponseReturned()
    {
        string pid = _mockUserInfo.Pid;
        var getInsurance = new GetInsurance(_context);

        var insurances = await getInsurance.Handle(pid);

        Assert.NotNull(insurances);
        Assert.NotNull(insurances.CarInsurance);
        Assert.IsType<InsuranceDataResponse>(insurances);
        Assert.Equal(2, insurances.CarInsurance?.Count());
        Assert.Equal(_mockCarInsurance[0].InsuranceId, insurances?.CarInsurance?[0].InsuranceId);
        Assert.Equal(_mockCarInsurance[1].InsuranceId, insurances?.CarInsurance?[1].InsuranceId);
    }

    public void Dispose() => _context.Dispose();
}
