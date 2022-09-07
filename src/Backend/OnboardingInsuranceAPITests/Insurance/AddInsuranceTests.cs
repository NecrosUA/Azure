namespace OnboardingInsuranceAPITests.Insurance;

public class AddInsuranceTests : IDisposable
{
    private readonly DataContext _context;

    private readonly UserInfo _userInfo = new UserInfo
    {
        Pid = "dcc0e27e-d8a4-443e-9d03-7d1ba1f3b5b6",
        Name = "Adam",
        Surname = "Jensen",
        Birthdate = new DateOnly(1993, 03, 09),
        BirthNumber = "9303091234",
        MobileNumber = "77422914",
        Email = "Adam.Jensen@dex.cz",
        Address1 = "Zeleň 43/1",
        Address2 = "Praha - Překážka",
        ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
    };

    private readonly InsuranceDataRequest _requestedData = new InsuranceDataRequest
    {
        Pid = "dcc0e27e-d8a4-443e-9d03-7d1ba1f3b5b6",
        CarInsurance = new CarInsuranceData
        {
            InsuranceId = "140b6414-fc66-4df6-906a-452e251be123",
            CarBarnd = "Škoda",
            CarType = "SportCar",
            Crashed = true,
            FirstOwner = true,
            InformationNote = "Confident driver",
            ExpirationDate = null,
            LastService = new DateOnly(2022, 01, 01),
            YearOfProduction = 2020,
            YearlyContribution = null
        }
    };

    public AddInsuranceTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName : "AddInsuranceTestsData")
            .Options;

        _context = new DataContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Users.Add(_userInfo);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_NullPid_ExceptionReturned()
    {
        string pid = default!;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData;

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(_requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_NullYearOfProduction_ExceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with {YearOfProduction = null}
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Theory]
    [InlineData(1900)]
    [InlineData(0)]
    [InlineData(2300)]
    public async Task Handle_WrongYearOfProduction_exceptionReturned(int yearOfProduction)
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { YearOfProduction = yearOfProduction }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.ValidationFailed, apiException.ErrorCode);
    }

    [Theory]
    [InlineData("1900-01-01")]
    [InlineData("2900-01-01")]
    [InlineData("1800-01-01")]
    public async Task Handle_WrongExpirationDate_exceptionReturned(string expirationDate)
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var parsedDate = DateOnly.ParseExact(expirationDate, "yyyy-MM-dd");
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { ExpirationDate = parsedDate }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.ValidationFailed, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_CorrectDataPassed_DataAddedToDb()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var getInsurance = new GetInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { ExpirationDate = new DateOnly(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day) }
        };

        await addInsurance.Handle(requestedData, pid);
        var insurances = await getInsurance.Handle(pid);

        Assert.NotNull(insurances);
        Assert.NotNull(insurances.CarInsurance);
        Assert.IsType<InsuranceDataResponse>(insurances);
        Assert.Equal(1, insurances.CarInsurance?.Count);
    }

    public void Dispose() => _context.Dispose();
}
