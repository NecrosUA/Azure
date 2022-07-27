using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Services;
using System;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class RegisterUserHandler : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<RegisterUserHandler> _log;

    public RegisterUserHandler(DataContext context, ILogger<RegisterUserHandler> log)
    {
        _context = context;
        _log = log;
    }

    public async Task CreateUser(string pid, string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Pid == pid);//check if user exist
        if (user == null)
        {
            await _context.AddAsync(new UserInfo
            {   Pid = pid,
                ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg",
                Email = email
            });
            await _context.SaveChangesAsync();
            _log.LogInformation($"New user registered. With pid:  {pid}");
        }
    }
}
