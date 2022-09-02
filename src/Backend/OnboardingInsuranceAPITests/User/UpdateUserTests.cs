namespace OnboardingInsuranceAPITests.User;

public class UpdateUserTests : IDisposable
{
    private readonly DataContext _context;

    private readonly UserData _userData = new UserData
    {
        Birthdate = new DateOnly(1993,03,09),
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
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handle_WrongPid_ExceptionReturned()
    {
        var pid = "wrong pid";
        UserData item = null!;
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("NotFound", exception.Message);
    }

    [Fact]
    public async Task Handle_NullBirthDate_exceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { Birthdate = null };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handler_1893BirthDate_ExceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { Birthdate = new DateOnly(1893,03,09) };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handler_2011BirthDate_ExceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { Birthdate = new DateOnly(2011, 03, 09) };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handler_8CharMobileNumber_ExceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { MobileNumber = "12345678X" };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handler_9CharOnlyMobileNumber_ExceptionReturned()
    {
        var pid = _userInfo.Pid;
        var item = _userData with { MobileNumber = "XXXXXXXXX" };
        var updateUser = new UpdateUser(_context);

        var exception = await Record.ExceptionAsync(() => updateUser.Handle(item, pid));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("ValidationFailed", exception.Message);
    }

    [Fact]
    public async Task Handler_CorrectData_UpdateDataInDb()
    {
        var pid = _userInfo.Pid;
        var item = _userData;
        var updateUser = new UpdateUser(_context);
        var getUser = new GetUser(_context);

        await updateUser.Handle(item, pid);
        var user = await getUser.Handle(pid);

        Assert.NotNull(user);
        Assert.IsType<UserData>(user);
        Assert.Equal(pid, user.Pid);
        Assert.Equal("Mock", user.Name);
        Assert.Equal("User", user.Surname);
        Assert.Equal("9303091234", user.BirthNumber); //Birthnumber should't change because we do not updated it
    }

    public void Dispose() => _context.Dispose();
}
