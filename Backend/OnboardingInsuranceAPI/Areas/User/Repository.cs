using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;
using OnboardingInsuranceAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User
{
    public class Repository
    {
        //private readonly DataContext context = new DataContext();

        public async Task<UserInfo> GetUserBy(string pid)
        {
            using var context = new DataContext();
            //await context.Database.EnsureDeletedAsync();
            //await context.Database.EnsureCreatedAsync();

            return await context.Users.FirstAsync(u => u.Pid == pid);
        }

        public async void UpdateItem(RequestedData item)
        {
            using var context = new DataContext();

            /*Prepare to update only not null data*/
            UserInfo user = await context.Users.FirstAsync(u => u.Pid == item.Pid);
            //user.Pid = item.Pid;
            user.Name = item.Name;
            user.Surname = item.Surname;
            user.Address1 = item.Address1;
            user.Address2 = item.Address2;
            user.Email = item.Email;
            user.MobileNumber = item.MobileNumber;
            user.ProfileImage = item.ProfileImage;

            context.Update(user);
            await context.SaveChangesAsync();
            Console.WriteLine("Information saved...");
        }

        public async void InsertItem(UserRegistration item)
        {
            var newUser = new UserInfo();

            newUser.Pid = item.Pid;
            newUser.Name = item.Name;
            newUser.Surname = item.Surname;
            newUser.Birthdate = item.Birthdate;
            newUser.BirthNumber = item.BirthNumber;
            newUser.MobileNumber = item.MobileNumber;
            newUser.Email = item.Email;
            newUser.Address1 = item.Address1;
            newUser.Address2 = item.Address2; 
            newUser.ProfileImage = item.ProfileImage;

            using var context = new DataContext();
            await context.AddAsync(newUser);
            Console.WriteLine("New uer registered...");
        }

    }
}
