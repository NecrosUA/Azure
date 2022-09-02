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
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handle_NullEmail_ExceptionReturned()
    {
        string pid = "some pid";
        string email = default!;
        var registerUser = new RegisterUser(_context);

        var exception = await Record.ExceptionAsync(() => registerUser.Handle(pid, email));

        Assert.NotNull(exception);
        Assert.IsType<ApiException>(exception);
        Assert.Equal("InvalidQueryParameters", exception.Message);
    }

    [Fact]
    public async Task Handle_WrongEmail_ExceptionReturned()
    {
        string pid = "some pid";
        string[] email = { "wrong email", "WrongEmail", "wrong@email", "wrong@email.", "wrong@e.mail" };
        var registerUser = new RegisterUser(_context);

        foreach (var mail in email)
        {
            var exception = await Record.ExceptionAsync(() => registerUser.Handle(pid, mail));

            Assert.NotNull(exception);
            Assert.IsType<ApiException>(exception);
            Assert.Equal("InvalidQueryParameters", exception.Message);
        }
    }

    [Fact]
    public async Task Handle_CorrectDataPassed_DataAddedToDb()
    {
        var pid = Guid.NewGuid().ToString();
        var email = "correct@email.cz";
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
