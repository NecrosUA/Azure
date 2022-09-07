namespace OnboardingInsuranceAPITests.User;

public class UpdateUserTests : IDisposable
{
    private readonly DataContext _context;

    private readonly UserData _userData = new UserData
    {
        Birthdate = new DateOnly(1993, 03, 09),
        Name = "Mock",
        Surname = "User",
        MobileNumber = "774229145",
        Email = "Adam.Jensen@dex.cz",
        Address1 = "Zeleň 43/1",
        Address2 = "Praha - Překážka",
        ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
    };

    private readonly UserInfo _userInfo = new UserInfo
    {
        Pid = "dcc0e27e-d8a4-443e-9d03-7d1ba1f3b5b6",
        Name = "Adam",
        Surname = "Jensen",
        Birthdate = new DateOnly(1993, 03, 09),
        BirthNumber = "9303091234",
        MobileNumber = "774229145",
        Email = "Adam.Jensen@dex.cz",
        Address1 = "Zeleň 43/1",
        Address2 = "Praha - Překážka",
        ProfileImage = "https://rostupload.blob.core.windows.net/images/adam.jpg"
    };

    public UpdateUserTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "UpdateUserTestsData")
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
        UserData item = null!;
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_WrongPid_ExceptionReturned()
    {
        var pid = "wrong pid";
        UserData item = null!;
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.NotFound, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_NullBirthDate_exceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { Birthdate = null };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Theory]
    [InlineData("1893-03-01")]
    [InlineData("2018-01-01")]
    [InlineData("2032-01-01")]
    public async Task Handler_WrongBirthDate_ExceptionReturned(string wrongBirthDate)
    {
        var pid = _userInfo.Pid;
        var parsedDate = DateOnly.ParseExact(wrongBirthDate, "yyyy-MM-dd");
        var item = _userData with { Birthdate = parsedDate };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.ValidationFailed, apiException.ErrorCode);
    }

    [Theory]
    [InlineData("12345678X")]
    [InlineData("XXXXXXXXX")]
    [InlineData("1234567")]
    [InlineData("123456")]
    [InlineData("12345")]
    [InlineData("774345353a")]
    public async Task Handler_WrongMobileNumber_ExceptionReturned(string mobileNumber)
    {
        var pid = _userInfo.Pid;
        var item = _userData with { MobileNumber = mobileNumber};
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.ValidationFailed, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handler_CorrectData_UpdateDataInDb()
    {
        var pid = _userInfo.Pid;
        var item = _userData;
        var expectedUser = _userData with
        {
            Pid = _userInfo.Pid, 
            BirthNumber = _userInfo.BirthNumber,
        };
        var updateUser = new UpdateUser(_context);
        var getUser = new GetUser(_context);

        await updateUser.Handle(item, pid);
        var user = await getUser.Handle(pid);

        Assert.NotNull(user);
        Assert.IsType<UserData>(user);
        Assert.Equal(pid, user.Pid);
        Assert.Equal(expectedUser, user); 
    }

    public void Dispose() => _context.Dispose();
}
