using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class RegisterUser : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<RegisterUser> _logger;

    public RegisterUser(DataContext context, ILogger<RegisterUser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(string pid, string email, HttpRequestData req)
    {
        if (string.IsNullOrEmpty(pid))
        {
            _logger.LogWarning("Error, received pid is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("Error, received email is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var sub = req.ReadPidFromJwt(); //secure 
        if (sub != pid)
        {
            _logger.LogWarning("Error, unauthorized access pid does not match with header");
            throw new ApiException(ErrorCode.Unauthorized);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid); //If this pid does not exist than add it to cosmos db
        if (user == null)
        {
            _context.Add(new UserInfo
            {
                Pid = pid,
                ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg",
                Email = email
            });

            await _context.SaveChangesAsync();
        }
    }
}
