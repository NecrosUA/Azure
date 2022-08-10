using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class GetUser : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<GetUser> _logger;

    public GetUser(DataContext context, ILogger<GetUser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<UserData> Handle(string pid)
    {
        if (string.IsNullOrEmpty(pid))
        {
            _logger.LogWarning("Error, received pid is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var users = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);

        if (users is null)
        {
            _logger.LogWarning("Error reading user, user is null!");
            throw new ApiException(ErrorCode.NotFound);
        }

        return new UserData
        {
            Name = users.Name,
            Email = users.Email,
            BirthNumber = users.BirthNumber,
            Birthdate = users.Birthdate,
            Address2 = users.Address2,
            Address1 = users.Address1,
            MobileNumber = users.MobileNumber,
            Surname = users.Surname,
            Pid = users.Pid,
            ProfileImage = users.ProfileImage,
        };
    }
}

