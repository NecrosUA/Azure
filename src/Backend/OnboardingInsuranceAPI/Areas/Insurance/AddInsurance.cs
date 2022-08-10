using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
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

    public async Task Handle(InsuranceData item)
    {
        if (string.IsNullOrEmpty(item.Pid))
        {
            _logger.LogWarning("Error, received pid is empty!");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == item.Pid);

        if (user is null)
        {
            _logger.LogWarning("Error reading user, user is null!");
            throw new ApiException(ErrorCode.NotFound);
        }

        user.CarInsurance = item.CarInsurance;
        _context.Update(user);
        await _context.SaveChangesAsync();
    }
}
