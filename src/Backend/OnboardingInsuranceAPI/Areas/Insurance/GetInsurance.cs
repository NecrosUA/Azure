using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class GetInsurance : IHandler
{
    private readonly DataContext _context;
    public GetInsurance(DataContext context)
    {
        _context = context;
    }

    public async Task<InsuranceData> GetInsuranceByPid(string pid)
    {
        var context =  await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);

        return new InsuranceData
        {
            ProfileImage = context.ProfileImage,
            CarInsurance = context.CarInsurance,
            Email = context.Email,
            Name = context.Name,
            Pid = context.Pid
        };
    }

}
