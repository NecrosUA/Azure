namespace OnboardingInsuranceAPITests.User;

public class GetUserTests : IDisposable
{
    private readonly DataContext _context;

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

    public GetUserTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "GetUserTestsData")
            .Options;

        _context = new DataContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        _context.Users.Add(_mockUserInfo);
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_NullPid_exceptionReturned()
    {
        string pid = default!;
        var getUser = new GetUser(_context);

        var exception = await Record.ExceptionAsync(() => getUser.Handle(pid));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(399, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_WrongPid_ExceptionReturned()
    {
        string pid = "wrong pid";
        var getUser = new GetUser(_context);

        var exception = await Record.ExceptionAsync(() => getUser.Handle(pid));

        Assert.NotNull(_context);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(404, (int)apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_CorrectPid_CorrectData()
    {
        string pid = _mockUserInfo.Pid;
        var getUser = new GetUser(_context);

        var user = await getUser.Handle(pid);

        Assert.NotNull(user);
        Assert.IsType<UserData>(user);
        Assert.Equal(pid, user.Pid);
    }

    public void Dispose() => _context.Dispose();
}
