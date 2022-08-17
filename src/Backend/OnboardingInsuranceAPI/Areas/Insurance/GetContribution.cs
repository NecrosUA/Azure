using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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

    public async Task<ContributionDataResponse> Handle(ContributionDataRequest requestedData, string pid)
    {
        if(string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var birthDate = _context.Users.FirstOrDefault(u => u.Pid == pid)?.Birthdate;
        if(string.IsNullOrEmpty(birthDate))
            throw new ApiException(ErrorCode.NotFound);

        var age = DateTime.Now.Year - DateOnly.Parse(birthDate).Year; 
        var carProductionYear = DateOnly.Parse(requestedData.YearOfProduction).Year;
        var carTypeContribution = (int)requestedData.CarType;
        var riskCoeficient = requestedData.Crashed && requestedData.FirstOwner ? 1.1 : 1;

        var responsedData = new ContributionDataResponse
        {
            YearlyContribution = Math.Round((carTypeContribution + carProductionYear * 2.3 - age * 30) * riskCoeficient, 2),
            ExpirationDate = new DateOnly(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day).ToString("yyy-MM-dd")
        };

        return responsedData;
    }
}
