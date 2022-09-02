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

    public UpdateUser(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(UserData item, string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
        if (user is null)
            throw new ApiException(ErrorCode.NotFound);

        if (item.Birthdate is null)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if (item.Birthdate <= DateOnly.Parse("1900-01-01"))
            throw new ApiException(ErrorCode.ValidationFailed);

        var age = DateTime.Now.Year - item.Birthdate.Value.Year;
        if (age < 18)
            throw new ApiException(ErrorCode.ValidationFailed);

        if (double.TryParse(item.MobileNumber, out _) == false || item.MobileNumber.Length != 9)
            throw new ApiException(ErrorCode.ValidationFailed);

        // Prepare to update only not null data
        if(string.IsNullOrEmpty(item.Name) == false) user.Name = item.Name;
        if (string.IsNullOrEmpty(item.Surname) == false) user.Surname = item.Surname;
        if (string.IsNullOrEmpty(item.Address1) == false) user.Address1 = item.Address1;
        if (string.IsNullOrEmpty(item.Address2) == false) user.Address2 = item.Address2;
        if (string.IsNullOrEmpty(item.MobileNumber) == false) user.MobileNumber = item.MobileNumber;
        if (string.IsNullOrEmpty(item.ProfileImage) == false) user.ProfileImage = item.ProfileImage;
        if(string.IsNullOrEmpty(user.BirthNumber)) user.BirthNumber = item.BirthNumber;
        if (user.Birthdate is null) user.Birthdate = item.Birthdate; 

        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
