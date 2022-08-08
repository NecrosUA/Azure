using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class PutInsurance: IHandler
{
    private readonly DataContext _context;

    public PutInsurance(DataContext context)
    {
        _context = context; 
    }

    public async Task UpdateInsurance(InsuranceData item)
    {
        var insurance = await _context.Users.FirstOrDefaultAsync(i => i.Pid == item.Pid);

        insurance.CarInsurance = item.CarInsurance;
        _context.Update(insurance);
        await _context.SaveChangesAsync();
        Console.WriteLine("Information saved...");
    }

}
