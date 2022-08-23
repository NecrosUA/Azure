using System;
using System.Linq;
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

    public ContributionDataResponse Handle(ContributionDataRequest requestedData, string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var birthDate = _context.Users.FirstOrDefault(u => u.Pid == pid)?.Birthdate;
        if (birthDate is null)
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        if (birthDate <= DateOnly.Parse("1900-01-01"))
            throw new ApiException(ErrorCode.ValidationFailed);

        var age = DateTime.Now.Year - birthDate.Value.Year;
        if (age < 18)
            throw new ApiException(ErrorCode.ValidationFailed);

        var yearProd = requestedData.YearOfProduction;
        if (yearProd is null)
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        if (yearProd <= 1900 || yearProd == 0 || yearProd > DateTime.Now.Year)
            throw new ApiException(ErrorCode.ValidationFailed);

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
