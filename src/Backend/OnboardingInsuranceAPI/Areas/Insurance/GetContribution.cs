using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.Enums;
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

    public async Task<ContributionData> Handle(ContributionData requestedData, string pid)
    {
        if(string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var birthDate = _context.Users.FirstOrDefault(u => u.Pid == pid).Birthdate;
        if(string.IsNullOrEmpty(birthDate))
            throw new ApiException(ErrorCode.NotFound);

        var age = DateTime.Now.Year - DateTime.Parse(birthDate).Year;
        var carProductionYear = DateTime.Parse(requestedData.YearOfProduction).Year;
        var carTypeContribution = Convert.ToInt32(CarTypesContribution.FromString(requestedData.CarType));
        var riskCoeficient = requestedData.Crashed && requestedData.FirstOwner ? 1.1 : 1;

        requestedData.YearlyContribution = Math.Round((carTypeContribution + carProductionYear * 2.3 - age*30) * riskCoeficient, 2);
        requestedData.ExpirationDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day).ToString(); //Add 1 year insurance to current date

        return requestedData;
    }
}
