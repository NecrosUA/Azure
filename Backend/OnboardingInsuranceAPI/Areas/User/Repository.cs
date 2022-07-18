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

        public async void CreateUser(string pid) 
        { 
            using var context = new DataContext();
            var user = await context.Users.FirstOrDefaultAsync(x => x.Pid == pid);//check if user exist
            if (user == null)
            {
                await context.AddAsync(new UserInfo { Pid = pid });
                await context.SaveChangesAsync();
                Console.WriteLine("New user registered!");
            }

        }

    }
}
