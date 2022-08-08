using Microsoft.EntityFrameworkCore;
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
    public GetUser(DataContext context)
    {
        _context = context;
    }

    public async Task<UserData> GetUserBy(string pid)
    {
        //await _context.Database.EnsureDeletedAsync(); //uncomment it in case DB is empty
        //await _context.Database.EnsureCreatedAsync();

        var users = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);

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

