using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Services;
using System;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User;

public class RegisterUserHandler : IUserHandler
{
    private readonly DataContext _context;

    public RegisterUserHandler(DataContext context)
    {
        _context = context;  
    }

    public async Task CreateUser(string pid)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Pid == pid);//check if user exist
        if (user == null)
        {
            await _context.AddAsync(new UserInfo { Pid = pid });
            await _context.SaveChangesAsync();
            Console.WriteLine("New user registered!");
        }

        //public async void InsertItem(UserRegistrationData item) //TODO rewrite this to complete registartion
        //{
        //    var newUser = new UserInfo
        //    {
        //        Pid = item.Pid,
        //        Name = item.Name,
        //        Surname = item.Surname,
        //        Birthdate = item.Birthdate,
        //        BirthNumber = item.BirthNumber,
        //        MobileNumber = item.MobileNumber,
        //        Email = item.Email,
        //        Address1 = item.Address1,
        //        Address2 = item.Address2,
        //        ProfileImage = item.ProfileImage
        //    };



        //    using var context = new DataContext();
        //    await context.AddAsync(newUser);
        //    Console.WriteLine("New user registered...");
        //}
    }
}
