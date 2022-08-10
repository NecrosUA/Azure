using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;
using System;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class RegisterUser : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<RegisterUser> _log;

    public RegisterUser(DataContext context, ILogger<RegisterUser> log)
    {
        _context = context;
        _log = log;
    }

    public async Task Handle(string pid, string email)
    {
        if (string.IsNullOrEmpty(pid))
        {
            _log.LogWarning("Error, received pid is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        if (string.IsNullOrEmpty(email))
        {
            _log.LogWarning("Error, received email is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
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
