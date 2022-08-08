using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Services;
using System;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class PostUser : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<PostUser> _log;

    public PostUser(DataContext context, ILogger<PostUser> log)
    {
        _context = context;
        _log = log;
    }

    public async Task CreateUser(string pid, string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Pid == pid);//check if user exist
        if (user == null)
        {
            _context.Add(new UserInfo
            {
                Pid = pid,
                ProfileImage = "https://rostupload.blob.core.windows.net/images/default.jpg",
                Email = email
            });

            await _context.SaveChangesAsync();
            _log.LogInformation($"New user registered. With pid:  {pid}");
        }
    }
}
