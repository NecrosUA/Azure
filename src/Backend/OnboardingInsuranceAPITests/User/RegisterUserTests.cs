namespace OnboardingInsuranceAPITests.User;

public class RegisterUserTests : IDisposable
{
    private readonly DataContext _context;

    public RegisterUserTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "RegisterUserTestsData")
            .Options;

        _context = new DataContext(options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task Handle_NullPid_ExceptionReturned()
    {
        string pid = default!;
        string email = default!;
        var registerUser = new RegisterUser(_context);

        var exception = await Record.ExceptionAsync(() => registerUser.Handle(pid, email));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Fact]
    public async Task Handle_NullEmail_ExceptionReturned()
    {
        string pid = "some pid";
        string email = default!;
        var registerUser = new RegisterUser(_context);

        var exception = await Record.ExceptionAsync(() => registerUser.Handle(pid, email));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Theory]
    [InlineData("wrong email")]
    [InlineData("WrongEmail")]
    [InlineData("wrong@email")]
    [InlineData("wrong@email.")]
    [InlineData("wrong@e.mail")]
    public async Task Handle_WrongEmail_ExceptionReturned(string email)
    {
        string pid = "some pid";
        var registerUser = new RegisterUser(_context);

        var exception = await Record.ExceptionAsync(() => registerUser.Handle(pid, email));

        Assert.NotNull(exception);
        var apiException = Assert.IsType<ApiException>(exception);
        Assert.Equal(ErrorCode.InvalidQueryParameters, apiException.ErrorCode);
    }

    [Theory]
    [InlineData("correct@email.cz")]
    [InlineData("correct@email.com")]
    [InlineData("correct@email.net")]
    [InlineData("crct@eml.org")]
    public async Task Handle_CorrectDataPassed_DataAddedToDb(string email)
    {
        var pid = Guid.NewGuid().ToString();
        var registerUser = new RegisterUser(_context);
        var getUser = new GetUser(_context);

        await registerUser.Handle(pid, email);
        var user = await getUser.Handle(pid);

        Assert.NotNull(user);
        Assert.IsType<UserData>(user);
        Assert.Equal(pid, user.Pid);
    }

    public void Dispose() => _context.Dispose();
}
