using Microsoft.EntityFrameworkCore;
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
    public async Task UpdateItem(UserData item)
    {
        /*Prepare to update only not null data*/
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);
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

