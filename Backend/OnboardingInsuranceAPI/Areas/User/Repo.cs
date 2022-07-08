using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnboardingInsuranceAPI.Areas.User
{
    public class Repo
    {
        //private readonly DataContext context = new DataContext();

        public async Task<UserInfo> GetUserBy(string pid)
        {
            using var context = new DataContext();
            //await context.Database.EnsureDeletedAsync();
            //await context.Database.EnsureCreatedAsync();

            return await context.Users.FirstAsync(u => u.Pid == pid);
        }

        public async void UpSertItem(ReqData item)
        {
            using var context = new DataContext();

            /*Prepare to update only not null data*/
            UserInfo user = await context.Users.FirstAsync(u => u.Pid == item.ReqPid);
            user.Pid = item.ReqPid;
            user.Name = item.ReqName;
            user.Surname = item.ReqSurname;
            user.Address1 = item.ReqAddress1;
            user.Address2 = item.ReqAddress2;
            user.Email = item.ReqEmail;
            user.MobileNumber = item.ReqMobileNumber;
            user.ProfileImage = item.ReqProfileImage;

            context.Update(user);
            await context.SaveChangesAsync();
            Console.WriteLine("Information saved...");
        }

        public void Save()
        {
            throw new NotImplementedException();
        }


    }
}
