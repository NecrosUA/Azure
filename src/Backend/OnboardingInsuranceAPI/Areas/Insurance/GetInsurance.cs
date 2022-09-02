using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class GetInsurance : IHandler
{
    private readonly DataContext _context;

    public GetInsurance(DataContext context)
    {
        _context = context;
    }

    public async Task<InsuranceDataResponse> Handle(string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Pid == pid);
        if(user is null)
            throw new ApiException(ErrorCode.NotFound);

        var insurances = await _context.Insurances.AsNoTracking().Where(i => i.Pid == pid).ToListAsync();
        return new InsuranceDataResponse
        {
            ProfileImage = user.ProfileImage,
            CarInsurance = insurances.Select(x => new CarInsuranceData()
            {
                ExpirationDate = x.ExpirationDate,
                InformationNote = x.InformationNote,
                YearlyContribution = x.YearlyContribution,
                CarType = x.CarType,
                CarBarnd = x.CarBarnd,                
                Crashed = x.Crashed,                
                FirstOwner = x.FirstOwner,
                LastService = x.LastService,
                YearOfProduction = x.YearOfProduction,
                InsuranceId = x.InsuranceId
            }).ToList(),
            Email = user.Email,
            Name = user.Name,
            Pid = user.Pid
        };
    }
}
