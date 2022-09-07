using OnboardingInsuranceAPI.Enums;

namespace OnboardingInsuranceAPITests.Insurance;

public class GetContributionTests : IDisposable //Implement IDisposable to run method Dispose() at the end of test
{
    private readonly DataContext _context;

    private readonly ContributionDataRequest _requestedData = new ContributionDataRequest
    {
        CarType = CarTypes.SportCar,
        Crashed = false,
        FirstOwner = true,
        YearOfProduction = 2022
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

    private readonly UserInfo _mockDefaultUserInfo = new UserInfo
    {
        Pid = "6a891aca-60c9-4c70-87c4-6bd52f9f2421",
        Name = "",
        Surname = "",
        Birthdate = null,
        BirthNumber = "",
        MobileNumber = "",
        Email = "Mock.Usr@dex.cz",
        Address1 = "",
        Address2 = "",
        ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg"
    };

    public GetContributionTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetContributionTestsData")
            .Options;

        _context = new DataContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Users.Add(_mockUserInfo);
        _context.Users.Add(_mockDefaultUserInfo);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_NullPid_ExceptionReturned()
    {
        string pid = default!;
        var requestedData = _requestedData;
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(399, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_WrongPid_ExceptionReturned()
    {
        var pid = "wrong pid";
        var requestedData = _requestedData;
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(404, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_EmptyBirthDate_ExceptionReturned()
    {
        var pid = _mockDefaultUserInfo.Pid;
        var requestedData = _requestedData;
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(400, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_NullYEarOfProduction_ExceptionReturned()
    {
        var pid = _mockUserInfo.Pid;
        var requestedData = _requestedData with
        {
            YearOfProduction = null
        };
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(399, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_1900YearOfProduction_ExceptionReturned()
    {
        var pid = _mockUserInfo.Pid;
        var requestedData = _requestedData with
        {
            YearOfProduction = 1900
        };
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(400, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_0YearOfProduction_ExceptionReturned()
    {
        var pid = _mockUserInfo.Pid;
        var requestedData = _requestedData with
        {
            YearOfProduction = 0
        };
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(400, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_2030YearOfProduction_ExceptionReturned()
    {
        var pid = _mockUserInfo.Pid;
        var requestedData = _requestedData with
        {
            YearOfProduction = 2030
        };
        var getContribution = new GetContribution(_context);

        var exception = await Record.ExceptionAsync(() => getContribution.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(400, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_CorrectDataPassed_CorrectResponseReturned()
    {
        var pid = _mockUserInfo.Pid;
        var requestedData = _requestedData;
        var getContribution = new GetContribution(_context);

        var responsedData = await getContribution.Handle(requestedData, pid);

        Assert.NotNull(responsedData);
        Assert.IsType<ContributionDataResponse>(responsedData);
        Assert.Equal((decimal)4480.6, responsedData.YearlyContribution);
        Assert.Equal(responsedData.ExpirationDate, new DateOnly(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day));
    }

    public void Dispose() => _context.Dispose(); 
}