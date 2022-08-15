using System;
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

    public async Task Handle(UserData item, string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);
        if (user is null)
            throw new ApiException(ErrorCode.NotFound);

        if (DateTime.TryParse(item.Birthdate, out var birthDate) == false)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if (birthDate <= DateTime.Parse("01/01/1900"))
            throw new ApiException(ErrorCode.ValidationFailed);

        if(int.TryParse(item.MobileNumber, out _) == false && item.MobileNumber.Length != 9)
            throw new ApiException(ErrorCode.ValidationFailed);

        // Prepare to update only not null data
        user.Name = item.Name;
        user.Surname = item.Surname;
        user.Address1 = item.Address1;
        user.Address2 = item.Address2;
        user.MobileNumber = item.MobileNumber;
        user.ProfileImage = item.ProfileImage;
        if(string.IsNullOrEmpty(user.BirthNumber)) user.BirthNumber = item.BirthNumber;
        if (string.IsNullOrEmpty(user.Birthdate)) user.Birthdate = birthDate.ToString(); //TODO change type of property in the next branch

        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
