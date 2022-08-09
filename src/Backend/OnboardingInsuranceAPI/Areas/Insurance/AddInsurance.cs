using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class AddInsurance: IHandler
{
    private readonly DataContext _context;

    public AddInsurance(DataContext context)
    {
        _context = context; 
    }

    public async Task AddInsuranceBy(InsuranceData item)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);

        user.CarInsurance = item.CarInsurance;
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

}
