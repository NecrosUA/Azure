using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Extensions;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class AddInsurance: IHandler
{
    private readonly DataContext _context;

    public AddInsurance(DataContext context)
    {
        _context = context; 
    }

    public async Task Handle(InsuranceDataRequest request, string pid)
    {
        if(string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var yearProd = request.CarInsurance.YearOfProduction;
        if(yearProd is null)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if(yearProd <= 1900 || yearProd == 0 || yearProd > DateTime.Now.Year)
            throw new ApiException(ErrorCode.ValidationFailed);

        if(request.CarInsurance.ExpirationDate < DateOnly.FromDateTime(DateTime.Now))
            throw new ApiException(ErrorCode.ValidationFailed);

        await _context.AddAsync(new CarInsuranceInfo
        {
            Pid = pid,
            InsuranceId = Guid.NewGuid().ToString(),
            CarBarnd = request.CarInsurance.CarBarnd,
            CarType = request.CarInsurance.CarType,
            Crashed = request.CarInsurance.Crashed,
            ExpirationDate = request.CarInsurance.ExpirationDate,
            FirstOwner = request.CarInsurance.FirstOwner,
            InformationNote = request.CarInsurance.InformationNote,
            LastService = request.CarInsurance.LastService,
            YearlyContribution = request.CarInsurance.YearlyContribution,
            YearOfProduction = request.CarInsurance.YearOfProduction
        });
        
        await _context.SaveChangesAsync();
    }
}
