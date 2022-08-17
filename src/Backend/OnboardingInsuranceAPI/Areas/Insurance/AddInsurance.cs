using System;
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
    private readonly ILogger<AddInsurance> _logger;

    public AddInsurance(DataContext context, ILogger<AddInsurance> logger)
    {
        _context = context;
        _logger = logger;   
    }

    public async Task Handle(InsuranceData item, string pid)
    {
        if (string.IsNullOrEmpty(pid))
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
        if (user is null)
            throw new ApiException(ErrorCode.NotFound);

        if (DateTime.TryParse(item.CarInsurance.YearOfProduction, out var yearOfProduction) == false)
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        if (yearOfProduction <= DateTime.Parse("01/01/1900"))
            throw new ApiException(ErrorCode.ValidationFailed);

        if (DateTime.Parse(item.CarInsurance.ExpirationDate) < DateTime.Now)
            throw new ApiException(ErrorCode.ValidationFailed);

        user.CarInsurance.CarBarnd = item.CarInsurance.CarBarnd;
        user.CarInsurance.YearOfProduction = yearOfProduction.ToString(); //TODO change type of property to DateTime in the next branch
        user.CarInsurance.CarType = item.CarInsurance.CarType;
        user.CarInsurance.FirstOwner = item.CarInsurance.FirstOwner;
        user.CarInsurance.Crashed = item.CarInsurance.Crashed;
        user.CarInsurance.ExpirationDate = item.CarInsurance.ExpirationDate;
        user.CarInsurance.InformationNote = item.CarInsurance.InformationNote;
        user.CarInsurance.LastService = item.CarInsurance.LastService;
        user.CarInsurance.YearlyContribution = item.CarInsurance.YearlyContribution;

        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}