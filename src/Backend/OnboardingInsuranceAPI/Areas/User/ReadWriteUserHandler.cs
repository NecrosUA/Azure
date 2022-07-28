using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;
using OnboardingInsuranceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class ReadWriteUserHandler : IHandler
{
    private readonly DataContext _context;
    public ReadWriteUserHandler(DataContext context)
    {
        _context = context;
    }

    public Task<UserInfo> GetUserBy(string pid)
    {
        //await _context.Database.EnsureDeletedAsync();
        //await _context.Database.EnsureCreatedAsync();

        return _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
    }
    public async Task UpdateItem(RequestedData item)
    {
        //using var context = new DataContext();

        /*Prepare to update only not null data*/
        UserInfo user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);
        //user.Pid = item.Pid;
        user.Name = item.Name;
        user.Surname = item.Surname;
        user.Address1 = item.Address1;
        user.Address2 = item.Address2;
        user.Email = item.Email;
        user.MobileNumber = item.MobileNumber;
        user.ProfileImage = item.ProfileImage;
        user.BirthNumber = item.BirthNumber;
        user.Birthdate = item.Birthdate;

        _context.Update(user);
        await _context.SaveChangesAsync();
        Console.WriteLine("Information saved...");
    }
}

