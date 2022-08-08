using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.User;

public class PutUser : IHandler
{
    private readonly DataContext _context;
    public PutUser(DataContext context)
    {
        _context = context;
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
