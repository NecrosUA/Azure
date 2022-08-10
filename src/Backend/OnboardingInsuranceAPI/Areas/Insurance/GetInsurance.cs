using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnboardingInsuranceAPI.ErrorHandling;
using OnboardingInsuranceAPI.Services;

namespace OnboardingInsuranceAPI.Areas.Insurance;

public class GetInsurance : IHandler
{
    private readonly DataContext _context;
    private readonly ILogger<GetInsurance> _logger;

    public GetInsurance(DataContext context, ILogger<GetInsurance> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsuranceData> Handle(string pid)
    {
        if (string.IsNullOrEmpty(pid))
        {
            _logger.LogWarning("Error, received pid is empty");
            throw new ApiException(ErrorCode.InvalidQueryParameters);
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);

        if (user is null)
        {
            _logger.LogWarning("Error reading user, user is null!");
            throw new ApiException(ErrorCode.NotFound);
        }

        return new InsuranceData
        {
            ProfileImage = user.ProfileImage,
            CarInsurance = user.CarInsurance,
            Email = user.Email,
            Name = user.Name,
            Pid = user.Pid
        };
    }
}
