using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.User;

public class UpdateUser : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<UpdateUser> _logger;

    public UpdateUser(DataContext context, ILogger<UpdateUser> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(UserData item, HttpRequestData req)
    {
        if (string.IsNullOrEmpty(item.Pid))
        {
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var sub = req.ReadPidFromJwt(); 
        if (item.Pid != sub)
        {
            throw new ApiException(ErrorCode.Unauthorized);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);
        if (user is null)
        {
            throw new ApiException(ErrorCode.NotFound);
        }

        // Prepare to update only not null data
        user.Name = item.Name;
        user.Surname = item.Surname;
        user.Address1 = item.Address1;
        user.Address2 = item.Address2;
        user.MobileNumber = item.MobileNumber;
        user.ProfileImage = item.ProfileImage;
        if(string.IsNullOrEmpty(user.BirthNumber)) user.BirthNumber = item.BirthNumber;
        if (string.IsNullOrEmpty(user.Birthdate)) user.Birthdate = item.Birthdate;

        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
