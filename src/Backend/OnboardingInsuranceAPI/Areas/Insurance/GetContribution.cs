using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class GetContribution : IHandler
{
    private readonly DataContext _context;

    public GetContribution(DataContext context)
    {
        _context = context;
    }

    public async Task<ContributionDataResponse> Handle(ContributionDataRequest requestedData, string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Pid == pid);
        if (user == null)
            throw new ApiException(ErrorCode.NotFound);

        if (user.Birthdate is null)
            throw new ApiException(ErrorCode.ValidationFailed);

        var yearProd = requestedData.YearOfProduction;
        if (yearProd is null)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if (yearProd <= 1900 || yearProd == 0 || yearProd > DateTime.Now.Year)
            throw new ApiException(ErrorCode.ValidationFailed);

        var age = DateTime.Now.Year - user.Birthdate.Value.Year;
        var carTypeContribution = (int)requestedData.CarType;
        var riskCoeficient = requestedData.Crashed && requestedData.FirstOwner ? 1.1 : 1;

        var responsedData = new ContributionDataResponse
        {
            YearlyContribution = Math.Round((decimal)((carTypeContribution + yearProd * 2.3 - age * 30) * riskCoeficient), 2),
            ExpirationDate = new DateOnly(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day)
        };

        return responsedData;
    }
}
