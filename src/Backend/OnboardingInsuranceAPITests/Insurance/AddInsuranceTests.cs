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
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
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
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handle_1900YearOfProduction_exceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { YearOfProduction = 1900 }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handle_0YearOfProduction_exceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { YearOfProduction = 0 }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handle_2030YearOfProduction_exceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { YearOfProduction = 2030 }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handle_1900ExpirationDate_exceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { ExpirationDate = new DateOnly(1900,01,01)}
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handle_2900ExpirationDate_exceptionReturned()
    {
        var pid = _requestedData.Pid;
        var addInsurance = new AddInsurance(_context);
        var requestedData = _requestedData with
        {
            CarInsurance = _requestedData.CarInsurance! with { ExpirationDate = new DateOnly(2900, 01, 01) }
        };

        var exception = await Record.ExceptionAsync(() => addInsurance.Handle(requestedData, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
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
        Assert.Equal(1,insurances.CarInsurance?.Count());
    }

    public void Dispose() => _context.Dispose();
}
