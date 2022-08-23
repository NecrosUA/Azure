using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class GetUser : IHandler
{
    private readonly DataContext _context;

    public GetUser(DataContext context)
    {
        _context = context;
    }

    public async Task<UserData> Handle(string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
        if (user is null)
            throw new ApiException(ErrorCode.NotFound);

        return new UserData
        {
            Name = user.Name,
            Email = user.Email,
            BirthNumber = user.BirthNumber,
            Birthdate = user.Birthdate,
            Address2 = user.Address2,
            Address1 = user.Address1,
            MobileNumber = user.MobileNumber,
            Surname = user.Surname,
            Pid = user.Pid,
            ProfileImage = user.ProfileImage
        };
    }
}

