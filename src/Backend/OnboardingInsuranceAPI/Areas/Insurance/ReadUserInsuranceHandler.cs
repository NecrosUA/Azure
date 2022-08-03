using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.Areas.Shared;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class ReadUserInsuranceHandler : IHandler
{
    private readonly DataContext _context;
    public ReadUserInsuranceHandler(DataContext context)
    {
        _context = context;
    }

    public Task<UserInfo> GetInsuranceByPid(string pid)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
    }

}
