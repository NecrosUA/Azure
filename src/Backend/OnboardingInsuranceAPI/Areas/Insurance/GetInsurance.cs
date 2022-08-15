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
            throw new ApiException(ErrorCode.InvalidQueryParameters);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Pid == pid);
        if (user is null)
            throw new ApiException(ErrorCode.NotFound);

        return new InsuranceData
        {
            ProfileImage = user.ProfileImage,
            CarInsurance = new CarInsuranceData
            {
                CarBarnd = user.CarInsurance.CarBarnd,
                YearOfProduction = user.CarInsurance.YearOfProduction,
                CarType = user.CarInsurance.CarType,
                FirstOwner = user.CarInsurance.FirstOwner,
                Crashed = user.CarInsurance.Crashed,
                ExpirationDate = user.CarInsurance.ExpirationDate,
                InformationNote = user.CarInsurance.InformationNote,
                LastService = user.CarInsurance.LastService,
                YearlyContribution = user.CarInsurance.YearlyContribution,
            },
            Email = user.Email,
            Name = user.Name,
            Pid = user.Pid
        };
    }
}
