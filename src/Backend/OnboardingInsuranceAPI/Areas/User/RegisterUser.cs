using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    public async Task CreateUser(string pid, string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);//check if user exist
        if (user == null)
        {
            _context.Add(new UserInfo
            {
                Pid = pid,
                ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg",
                Email = email
            });

            await _context.SaveChangesAsync();
            //_log.LogInformation($"New user registered. With pid:  {pid}");
        }
    }
}
