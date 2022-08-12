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

    public async Task Handle(InsuranceData item, HttpRequestData req)
    {

        if (string.IsNullOrEmpty(item.Pid))
        {
            _logger.LogWarning("Error, received pid is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var sub = req.ReadPidFromJwt(); //secured 
        if (item.Pid != sub)
        {
            _logger.LogWarning("Error, unauthorized access pid does not match with header");
            throw new ApiException(ErrorCode.Unauthorized);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);
        if (user is null)
        {
            _logger.LogWarning("Error reading user, user is null!");
            throw new ApiException(ErrorCode.NotFound);
        }

        user.CarInsurance.CarBarnd = item.CarInsurance.CarBarnd;
        user.CarInsurance.Year = item.CarInsurance.Year;
        user.CarInsurance.CarType = item.CarInsurance.CarType;
        user.CarInsurance.FirstOwner = item.CarInsurance.FirstOwner;
        user.CarInsurance.Crashed = item.CarInsurance.Crashed;
        user.CarInsurance.ExpDate = item.CarInsurance.ExpDate;
        user.CarInsurance.InformationNote = item.CarInsurance.InformationNote;
        user.CarInsurance.LastService = item.CarInsurance.LastService;
        user.CarInsurance.YearlyContribution = item.CarInsurance.YearlyContribution;

        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
